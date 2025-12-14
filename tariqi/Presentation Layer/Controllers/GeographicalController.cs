using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tariqi.Application_Layer.DTOs.GeographicalDtos;
using tariqi.Application_Layer.Exceptions;
using tariqi.Application_Layer.Interfaces;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Repositories_Interfaces;

namespace tariqi.Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeographicalController : ControllerBase
    {
        private readonly IGeographicalService _geographicalService;

        public GeographicalController(IGeographicalService geographicalService)
        {
            _geographicalService = geographicalService;
        }


        // 1. Get Regions 
        [HttpGet("regions")] // GET: api/Geographical/regions
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetRegions()
            => Ok(await _geographicalService.GetAllRegionsAsync());

        // 2. Create Region
        [HttpPost("regions")] // POST: api/Geographical/regions
        public async Task<ActionResult<RegionDto>> CreateRegion([FromBody] CreateRegionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            try
            {
                var createdRegionDto = await _geographicalService.CreateRegionAsync(dto);

                return Ok(createdRegionDto);
            }
            catch (DomainValidationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // 3. Get Areas by Region id
        [HttpGet("{regionId}/areas")] // GET: api/Geographical/{regionId}/areas
        public async Task<ActionResult<IEnumerable<AreaDto>>> GetAreasByRegionId(int regionId)
        {
            try
            {
                var areas = await _geographicalService.FindAreasWithRegionAsync(regionId);
                return Ok(areas); 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // 4. Create Area
        [HttpPost("areas")] // POST: api/Geographical/areas
        public async Task<ActionResult<AreaDto>> CreateArea([FromBody] CreateAreaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            try
            {
                var createdAreaDto = await _geographicalService.CreateAreaAsync(dto);
                return Ok(createdAreaDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DomainValidationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // 5. Update Area   
        [HttpPut("areas/{id}")]
        public async Task<IActionResult> UpdateArea(int id,[FromBody] UpdateAreaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
            try
            {
                await _geographicalService.UpdateAreaAsync(id, dto);
                return Ok(new {message = "Area Updated Successfully" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DomainValidationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // 6. Delete Area
        [HttpDelete("areas/{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            try
            {
                var deleted = await _geographicalService.DeleteAreaAsync(id);

                if (deleted)
                    return Ok(new { message = "Area Deleted Successfully" });

                return BadRequest(new {message = "You Can`t Delete This Area becuase it is Assocciated with othor data!!" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
