using tariqi.Application_Layer.DTOs.Vehicle_DTOs;
using tariqi.Application_Layer.Interfaces;
using tariqi.Domain_Layer.Entities;
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

        public Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto dto, string currentUserId, string role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVehicleAsync(int vehicleId, string currentUserId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VehicleDto> GetVehicleByIdAsync(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VehicleDto>> GetVehiclesByAreaAsync(int areaId)
        {
            throw new NotImplementedException();
        }

        public Task<VehicleDto> UpdateVehicleAsync(int vehicleId, UpdateVehicleDto dto, string currentUserId, string role)
        {
            throw new NotImplementedException();
        }
    }
}
