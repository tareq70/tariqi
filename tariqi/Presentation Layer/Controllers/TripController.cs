using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tariqi.Application_Layer.DTOs.Trip_DTOs;
using tariqi.Application_Layer.Interfaces;

namespace tariqi.Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }


        // 1️. Search Trips
        [HttpGet]
        public async Task<IActionResult> SearchTrips(
            [FromQuery] int? originRegionId,
            [FromQuery] int? destinationRegionId,
            [FromQuery] DateTime? date)
        {
            var trips = await _tripService.SearchTripsAsync(
                originRegionId,
                destinationRegionId,
                date
            );

            return Ok(trips);
        }

        // =========================
        // 2️ Get Trip By Id
        // GET /api/trips/{id}
        // =========================
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TripDto>> GetTripById(int id)
        {
            var trip = await _tripService.GetTripByIdAsync(id);
            return Ok(trip);
        }

       
        // 3️. Create Trip
        // POST /api/trips
        [Authorize(Roles = "Driver,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var trip = await _tripService.CreateTripAsync(
                dto,
                userId!,
                role!
            );

            return CreatedAtAction(
                nameof(GetTripById),
                new { id = trip.Id },
                trip
            );
        }

        // 4️. Update Trip
        // PUT /api/trips/{id}
        [Authorize(Roles = "Driver,Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTrip(
            int id,
            [FromBody] CreateTripDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var updatedTrip = await _tripService.UpdateTripAsync(
                id,
                dto,
                userId!,
                role!
            );

            return Ok(updatedTrip);
        }


        // 5. Cancel Trip
        // POST /api/trips/{id}/cancel
        [Authorize(Roles = "Driver,Admin")]
        [HttpPost("{id:int}/cancel")]
        public async Task<IActionResult> CancelTrip(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            await _tripService.CancelTripAsync(id, userId!, role!);
            return Ok(new { message = "Trip cancelled successfully" });
        }

        // 6️. Get Trips By Vehicle
        // GET /api/vehicles/{id}/trips
        [HttpGet("/api/vehicles/{id:int}/trips")]
        public async Task<IActionResult> GetTripsByVehicle(int id)
        {
            var trips = await _tripService.GetTripsByVehicleAsync(id);
            return Ok(trips);
        }

        // 7️. Start Trip
        // POST /api/trips/{id}/start
        [Authorize(Roles = "Driver")]
        [HttpPost("{id:int}/start")]
        public async Task<IActionResult> StartTrip(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _tripService.StartTripAsync(id, userId!);

            return Ok(new { message = "Trip started successfully" });
        }

        // 8️. Complete Trip
        // POST /api/trips/{id}/complete
        [Authorize(Roles = "Driver")]
        [HttpPost("{id:int}/complete")]
        public async Task<IActionResult> CompleteTrip(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _tripService.CompleteTripAsync(id, userId!);

            return Ok(new { message = "Trip completed successfully" });
        }
    }
}
