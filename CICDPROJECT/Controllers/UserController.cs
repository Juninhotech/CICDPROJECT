using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CICDPROJECT.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetName()
        {
            return Ok("Welcome to Juninho world of tech");
        }
    }
}
