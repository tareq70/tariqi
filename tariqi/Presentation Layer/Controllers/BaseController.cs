using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tariqi.Presentation_Layer.Responses;

namespace tariqi.Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> Success<T>(
            T data,
            string? message = null)
        {
            var traceIdentifier = HttpContext.TraceIdentifier;
            return Ok(ApiResponseFactory.Success(data, message, traceId: traceIdentifier));
        }
        protected ActionResult<ApiResponse<object>> Success(
            string? message = null)
        {
            var traceIdentifier = HttpContext.TraceIdentifier;
            return Ok(ApiResponseFactory.Success(message, traceId: traceIdentifier));
        }

    }
}
