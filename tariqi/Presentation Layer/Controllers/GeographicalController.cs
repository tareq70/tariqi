using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Repositories_Interfaces;

namespace tariqi.Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeographicalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GeographicalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // 1. Get Regions 
        [HttpGet("regions")] // GET: api/Geographical/regions
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
        {
            var regionRepo = _unitOfWork.GetRepository<Region>(); 
            var regions = await regionRepo.GetAllAsync();
            return Ok(regions);
        }
        
    }
}
