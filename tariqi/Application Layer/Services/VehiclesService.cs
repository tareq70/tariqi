using tariqi.Application_Layer.DTOs.Vehicle_DTOs;
using tariqi.Application_Layer.Interfaces;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Enums;
using tariqi.Domain_Layer.Repositories_Interfaces;

namespace tariqi.Application_Layer.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Vehicle> _vehiclesRepo;

        public VehiclesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _vehiclesRepo = _unitOfWork.GetRepository<Vehicle>();
        }

        public async Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto dto, string currentUserId, string role)
        {
            if (dto.SeatsCount <= 0)
                throw new Exception("Seats count must be greater than zero.");

            var existingVehicle = await _vehiclesRepo.FindAsync(v => v.PlateNumber == dto.PlateNumber);
            if (existingVehicle.Any())
                throw new Exception("Vehicle with this plate number already exists.");

            var area = await _unitOfWork.GetRepository<Area>().GetByIdAsync(dto.AreaId);
            if (area == null)
                throw new Exception("Area not found.");

            if (role == "Driver")
            {
                var driverVehicles = await _vehiclesRepo.FindAsync(v => v.DriverId == currentUserId && v.IsActive);
                if (driverVehicles.Any())
                    throw new Exception("Driver already has an active vehicle.");
            }
            var vehicle = new Vehicle
            {
                PlateNumber = dto.PlateNumber,
                Model = dto.Model,
                SeatsCount = dto.SeatsCount,
                DriverId = currentUserId,
                AreaId = dto.AreaId,
                IsActive = true
            };
            await _vehiclesRepo.AddAsync(vehicle);
            await _unitOfWork.SaveAsync();

            return new VehicleDto
            {
                Id = vehicle.Id,
                PlateNumber = vehicle.PlateNumber,
                Model = vehicle.Model,
                SeatsCount = vehicle.SeatsCount,
                IsActive = vehicle.IsActive,
                AreaName = area.Name
            };
        }

        public async Task DeleteVehicleAsync(int vehicleId, string currentUserId, string role)
        {
            var vehicle = await _vehiclesRepo.GetByIdAsync(vehicleId);

            if (vehicle == null)
                throw new Exception("Vehicle not found");

            if (!vehicle.IsActive)
                throw new Exception("Vehicle already deleted");

            vehicle.IsActive = false;

            _vehiclesRepo.Update(vehicle);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehiclesRepo.FindAsync(v => v.IsActive);
            return vehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                PlateNumber = v.PlateNumber,
                Model = v.Model,
                SeatsCount = v.SeatsCount,
                IsActive = v.IsActive,
                DriverId = v.DriverId,
                DriverName = v.Driver != null ? v.Driver.UserName : null
            });
        }

        public async Task<VehicleDto> GetVehicleByIdAsync(int vehicleId)
        {
            var vehicle = await _vehiclesRepo.GetByIdAsync(vehicleId);
            if (vehicle == null || !vehicle.IsActive)
                throw new Exception("Vehicle not found");
            return new VehicleDto
            {
                Id = vehicle.Id,
                PlateNumber = vehicle.PlateNumber,
                Model = vehicle.Model,
                SeatsCount = vehicle.SeatsCount,
                IsActive = vehicle.IsActive,
                DriverId = vehicle.DriverId,
                DriverName = vehicle.Driver != null ? vehicle.Driver.UserName : null
            };
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByAreaAsync(int areaId)
        {
            var vehicles = await _vehiclesRepo.FindAsync(v =>
            v.IsActive &&
            v.AreaId == areaId
            );

            return vehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                PlateNumber = v.PlateNumber,
                Model = v.Model,
                SeatsCount = v.SeatsCount,
                IsActive = v.IsActive,
                DriverId = v.DriverId,
                DriverName = v.Driver != null ? v.Driver.FullName : null
            });

        }

        public async Task<VehicleDto> UpdateVehicleAsync(int vehicleId, UpdateVehicleDto dto, string currentUserId, string role)
        {
            var vehicle = await _vehiclesRepo.GetByIdAsync(vehicleId);

            if (vehicle == null || !vehicle.IsActive)
                throw new Exception("Vehicle not found");

            // Authorization
            if (role == UserRole.Driver.ToString() && vehicle.DriverId != currentUserId)
                throw new Exception("Unauthorized");

            // Partial Update
            if (!string.IsNullOrWhiteSpace(dto.PlateNumber))
                vehicle.PlateNumber = dto.PlateNumber;

            if (!string.IsNullOrWhiteSpace(dto.Model))
                vehicle.Model = dto.Model;

            if (dto.SeatsCount.HasValue)
                vehicle.SeatsCount = dto.SeatsCount.Value;

            if (dto.AreaId.HasValue)
                vehicle.AreaId = dto.AreaId.Value;

            // Only Admin can change Driver
            if (role == UserRole.Admin.ToString() && dto.DriverId != null)
                vehicle.DriverId = dto.DriverId;

            _vehiclesRepo.Update(vehicle);
            await _unitOfWork.SaveAsync();

            return new VehicleDto
            {
                Id = vehicle.Id,
                PlateNumber = vehicle.PlateNumber,
                Model = vehicle.Model,
                SeatsCount = vehicle.SeatsCount,
                IsActive = vehicle.IsActive,
                DriverId = vehicle.DriverId,
                DriverName = vehicle.Driver?.FullName
            };
        }
    }
}
