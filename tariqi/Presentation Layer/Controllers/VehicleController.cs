using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tariqi.Application_Layer.DTOs.Vehicle_DTOs;
using tariqi.Application_Layer.Interfaces;
using tariqi.Application_Layer.Services;

namespace tariqi.Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehiclesService _vehiclesService;

        public VehicleController(IVehiclesService vehicleService)
        {
            _vehiclesService = vehicleService;
        }

        [HttpGet("GetAllVehicles")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _vehiclesService.GetAllVehiclesAsync());
        }

        [HttpGet("GetVehicleById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _vehiclesService.GetVehicleByIdAsync(id));
        }

        [HttpGet("GetVehicleByAreaId")]
        public async Task<IActionResult> GetByArea(int areaId)
        {
            return Ok(await _vehiclesService.GetVehiclesByAreaAsync(areaId));
        }

       // [Authorize]
        [HttpPut("UpdateVehicle")]
        public async Task<IActionResult> Update(int id, UpdateVehicleDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var result = await _vehiclesService.UpdateVehicleAsync(
                id,
                dto,
                userId!,
                role!
            );

            return Ok(result);
        }

       // [Authorize(Roles = "Admin")]
        [HttpPost("CreateVehicle")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            //if (userId == null || role == null)
            //{
            //    return Unauthorized();
            //}

            var vehicle = await _vehiclesService.CreateVehicleAsync(
                dto,
                userId!,
                role!
            );

            return CreatedAtAction(
                nameof(GetById),
                new { id = vehicle.Id },
                vehicle
            );
        }

        [HttpDelete("DeleteVehicle")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            await _vehiclesService.DeleteVehicleAsync(id, userId!, role!);
            return NoContent();
        }


    }
}
