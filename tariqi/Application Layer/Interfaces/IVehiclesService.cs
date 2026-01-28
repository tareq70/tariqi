using tariqi.Application_Layer.DTOs.Vehicle_DTOs;

namespace tariqi.Application_Layer.Interfaces
{
    public interface IVehiclesService
    {
        // Get all vehicles with Driver info
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();

        // Get specific vehicle with driver info
        Task<VehicleDto> GetVehicleByIdAsync(int vehicleId); 

        // Get all vehicles based on area id with driver info
        Task<IEnumerable<VehicleDto>> GetVehiclesByAreaAsync(int areaId);

        // Post vehicle with driver info
        Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto dto, string currentUserId, string role);

        // Update vehicle
        Task<VehicleDto> UpdateVehicleAsync(int vehicleId, UpdateVehicleDto dto, string currentUserId, string role);

        // Delete vehicle (soft delete)
        Task DeleteVehicleAsync(int vehicleId, string currentUserId, string role);

    }
}
