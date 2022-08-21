using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace REST_ServiceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("{code}")]
        [HttpGet]
        public IActionResult Error(int code)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = feature.Error;
            return new ObjectResult(new ApiResponse(code, ex.Message));
        }
    }
}
