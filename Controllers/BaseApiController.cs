using TodoApi.Core;
using Microsoft.AspNetCore.Mvc;
namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : Controller
    {
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return Ok();
            return BadRequest(result.Error);
        }
    }
}