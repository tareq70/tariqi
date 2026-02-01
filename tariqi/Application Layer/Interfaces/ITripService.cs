using tariqi.Application_Layer.DTOs.Trip_DTOs;

namespace tariqi.Application_Layer.Interfaces
{
    public interface ITripService
    {
        // Search available trips 
        Task<IEnumerable<TripDto>> SearchTripsAsync
            (int? originRegionId, int? destinationRegionId, DateTime? date); // Search available trips 
        // Get trip details by ID
        Task<TripDto> GetTripByIdAsync(int tripId); 
        // Create a new trip 
        Task<TripDto> CreateTripAsync(CreateTripDto dto, string currentUserId, string role);
        // Update an existing trip
        Task<TripDto> UpdateTripAsync(int tripId,CreateTripDto dto,string currentUserId,string role);

        // Cancel a trip [Driver | Admin]
        Task CancelTripAsync(int tripId,string currentUserId,string role);

        // Get All trips assigned to a specific vehicle
        Task<IEnumerable<TripDto>> GetTripsByVehicleAsync(int vehicleId);
        // Get All trips assigned to a specific driver
        Task<IEnumerable<TripDto>> GetTripsForDriverAsync(string driverId);
        // Get All trips booked by a specific passenger
        Task<IEnumerable<TripDto>> GetTripsForPassengerAsync(string passengerId);
        // Start a trip [Driver]
        Task StartTripAsync(int tripId, string currentUserId);
        // Complete a trip [Driver]
        Task CompleteTripAsync(int tripId, string currentUserId);
    }
}
