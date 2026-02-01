using tariqi.Application_Layer.DTOs.Trip_DTOs;
using tariqi.Application_Layer.Interfaces;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Enums;
using tariqi.Domain_Layer.Repositories_Interfaces;

namespace tariqi.Application_Layer.Services
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Trip> _tripRepo;
        private readonly IRepository<Vehicle> _vehicleRepo;
        private readonly IRepository<Booking> _bookingRepo;
        private readonly IRepository<Area> _areaRepo;


        public TripService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tripRepo = _unitOfWork.GetRepository<Trip>();
            _vehicleRepo = _unitOfWork.GetRepository<Vehicle>();
            _bookingRepo = _unitOfWork.GetRepository<Booking>();
            _areaRepo = _unitOfWork.GetRepository<Area>();
        }
        public async Task<IEnumerable<TripDto>> SearchTripsAsync(int? originRegionId, int? destinationRegionId, DateTime? date)
        {
            var trips = await _tripRepo.FindAsync(
                t => t.Status == TripStatus.Scheduled &&
                ((!date.HasValue || t.DepartureDateTime.Date == date.Value.Date)));

            return trips
                .Where(t =>
                    (!originRegionId.HasValue || t.OriginArea.RegionId == originRegionId) &&
                    (!destinationRegionId.HasValue || t.DestinationArea.RegionId == destinationRegionId)
                )
                .Select(t => MapToTripDto(t)); // private method to map Trip to TripDto (Helper Regions)
        }
        public async Task<TripDto> GetTripByIdAsync(int tripId)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId);
            if (trip == null)
                throw new Exception("Trip not found");

            return MapToTripDto(trip);
        }
        public async Task<TripDto> CreateTripAsync(CreateTripDto dto, string currentUserId, string role)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(dto.VehicleId);
            if (vehicle == null || !vehicle.IsActive)
                throw new Exception("Vehicle not found");

            if (role == "Driver" && vehicle.DriverId != currentUserId)
                throw new Exception("You are not allowed to create trip for this vehicle");

            if (dto.OriginAreaId == dto.DestinationAreaId)
                throw new Exception("Origin and Destination cannot be the same");

            if (dto.DepartureDateTime <= DateTime.UtcNow)
                throw new Exception("Departure time must be in the future");

            if (dto.PricePerSeat <= 0)
                throw new Exception("Invalid price");

            var originArea = await _areaRepo.GetByIdAsync(dto.OriginAreaId);
            var destinationArea = await _areaRepo.GetByIdAsync(dto.DestinationAreaId);

            if (originArea == null || destinationArea == null)
                throw new Exception("Invalid areas");

            var trip = new Trip
            {
                VehicleId = dto.VehicleId,
                OriginAreaId = dto.OriginAreaId,
                DestinationAreaId = dto.DestinationAreaId,
                OriginRegionId = originArea.RegionId,
                DestinationRegionId = destinationArea.RegionId,
                DepartureDateTime = dto.DepartureDateTime,
                EstimatedArrivalTime = dto.EstimatedArrivalTime,
                PricePerSeat = dto.PricePerSeat,
                CreatedBy = currentUserId,
                Status = TripStatus.Scheduled
            };

            await _tripRepo.AddAsync(trip);
            await _unitOfWork.SaveAsync();

            return MapToTripDto(trip);
        }
        public async Task<TripDto> UpdateTripAsync(int tripId, CreateTripDto dto, string currentUserId, string role)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId);
            if (trip == null)
                throw new Exception("Trip not found");

            if (trip.Status != TripStatus.Scheduled)
                throw new Exception("Only scheduled trips can be updated");

            if (role == "Driver" && trip.CreatedBy != currentUserId)
                throw new Exception("Unauthorized");


            trip.DepartureDateTime = dto.DepartureDateTime;
            trip.EstimatedArrivalTime = dto.EstimatedArrivalTime;
            trip.PricePerSeat = dto.PricePerSeat;

            _tripRepo.Update(trip);
            await _unitOfWork.SaveAsync();

            return MapToTripDto(trip);
        }
        public async Task CancelTripAsync(int tripId, string currentUserId, string role)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId);
            if (trip == null)
                throw new Exception("Trip not found");

            if (trip.Status == TripStatus.Completed)
                throw new Exception("Cannot cancel completed trip");

            if (role == "Driver" && trip.CreatedBy != currentUserId)
                throw new Exception("Unauthorized");

            trip.Status = TripStatus.Cancelled;

            var bookings = await _bookingRepo.FindAsync(b => b.TripId == tripId);
            foreach (var booking in bookings)
            {
                booking.Status = BookingStatus.Cancelled;
            }

            _tripRepo.Update(trip);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TripDto>> GetTripsByVehicleAsync(int vehicleId)
        {
            var trips = await _tripRepo.FindAsync(t => t.VehicleId == vehicleId);
            return trips.Select(MapToTripDto);
        }

        public async Task<IEnumerable<TripDto>> GetTripsForDriverAsync(string driverId)
        {
            var trips = await _tripRepo.FindAsync(t => t.Vehicle.DriverId == driverId);
            return trips.Select(MapToTripDto);
        }

        public async Task<IEnumerable<TripDto>> GetTripsForPassengerAsync(string passengerId)
        {
            var bookings = await _bookingRepo.FindAsync(b => b.PassengerId == passengerId);
            return bookings.Select(b => MapToTripDto(b.Trip)).Distinct();
        }
        public async Task StartTripAsync(int tripId, string currentUserId)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId);
            if (trip == null)
                throw new Exception("Trip not found");

            if (trip.Status != TripStatus.Scheduled)
                throw new Exception("Trip cannot be started");

            if (trip.Vehicle.DriverId != currentUserId)
                throw new Exception("Unauthorized");

            trip.Status = TripStatus.OnGoing;
            _tripRepo.Update(trip);
            await _unitOfWork.SaveAsync();
        }
        public async Task CompleteTripAsync(int tripId, string currentUserId)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId);
            if (trip == null)
                throw new Exception("Trip not found");

            if (trip.Status != TripStatus.OnGoing)
                throw new Exception("Trip is not ongoing");

            if (trip.Vehicle.DriverId != currentUserId)
                throw new Exception("Unauthorized");

            trip.Status = TripStatus.Completed;
            _tripRepo.Update(trip);
            await _unitOfWork.SaveAsync();
        }

        #region Helper Methods

        private TripDto MapToTripDto(Trip trip)
        {
            var bookedSeats = trip.Bookings?.Sum(b => b.SeatsCount) ?? 0;

            return new TripDto
            {
                Id = trip.Id,
                VehicleId = trip.VehicleId,
                VehiclePlate = trip.Vehicle.PlateNumber,
                OriginArea = trip.OriginArea.Name,
                DestinationArea = trip.DestinationArea.Name,
                DepartureDateTime = trip.DepartureDateTime,
                EstimatedArrivalTime = trip.EstimatedArrivalTime,
                PricePerSeat = trip.PricePerSeat,
                AvailableSeats = trip.Vehicle.SeatsCount - bookedSeats,
                Status = trip.Status
            };
        }

        #endregion
    }
}
