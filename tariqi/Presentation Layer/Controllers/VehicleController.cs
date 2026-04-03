using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tariqi.Application_Layer.DTOs.Vehicle_DTOs;
using tariqi.Application_Layer.Interfaces;
using tariqi.Application_Layer.Services;
using tariqi.Presentation_Layer.Responses;

namespace tariqi.Presentation_Layer.Controllers
{
    public class VehicleController : BaseController
    {
        private readonly IVehiclesService _vehiclesService;

        public VehicleController(IVehiclesService vehicleService)
        {
            _vehiclesService = vehicleService;
        }

        [HttpGet("GetAllVehicles")]
        public async Task<ActionResult<ApiResponse<IEnumerable<VehicleDto>>>> GetAll()
        {
            var vehicles = await _vehiclesService.GetAllVehiclesAsync();
            return Success(
                vehicles,
                vehicles.Any()
                ? "Vehicles retrieved successfully"
                : "No vehicles found");
        }

        [HttpGet("GetVehicleById")]
        public async Task<ActionResult<ApiResponse<VehicleDto>>> GetById(int id)
        {
            var vehicle = await _vehiclesService.GetVehicleByIdAsync(id);
            return Success(vehicle, "Vehicle retrieved successfully");
        }

        [HttpGet("GetVehicleByAreaId")]
        public async Task<ActionResult<ApiResponse<IEnumerable<VehicleDto>>>> GetByArea(int areaId)
        {
            var vehicles = await _vehiclesService.GetVehiclesByAreaAsync(areaId);
            return Success(
                vehicles,
                vehicles.Any()
                ? "Vehicles retrieved successfully"
                : "No vehicles found for the specified area");
        }

       // [Authorize]
        [HttpPut("UpdateVehicle")]
        public async Task<ActionResult<ApiResponse<VehicleDto>>> Update(int id, UpdateVehicleDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var result = await _vehiclesService.UpdateVehicleAsync(
                id,
                dto,
                userId!,
                role!
            );

            return Success(result, "Vehicle updated successfully");
        }

       // [Authorize(Roles = "Admin")]
        [HttpPost("CreateVehicle")]
        public async Task<ActionResult<ApiResponse<VehicleDto>>> Create([FromBody] CreateVehicleDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var vehicle = await _vehiclesService.CreateVehicleAsync(
                dto,
                userId!,
                role!
            );

            return Success(vehicle, "Vehicle created successfully");
        }

        [HttpDelete("DeleteVehicle")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            await _vehiclesService.DeleteVehicleAsync(id, userId!, role!);
            return Success("Vehicle deleted successfully");
        }


    }
}
