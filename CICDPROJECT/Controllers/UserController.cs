using CICDPROJECT.Model;
using CICDPROJECT.Model.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CICDPROJECT.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        LocationConfiguration _location;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _openWeatherApiKey;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(LocationConfiguration location, IHttpContextAccessor httpContextAccessor, IOptions<OpenWeatherOption> openWeatherOptions)
        {
            _location = location;
            _httpContextAccessor = httpContextAccessor;
            _openWeatherApiKey = openWeatherOptions.Value.ApiKey;
        }

        [HttpGet]
        public IActionResult GetName()
        {
            return Ok("Welcome to Juninho world of tech");
        }

        [HttpGet("GetAllUser")]
        public IActionResult GetAllUsers(string accessCode)
        {
            var isCodeMatch = UserData.code.AccessCodes.Contains(accessCode);
            if(isCodeMatch)
            {
                var get = UserData.Users;
                if (get.Count() == 0)
                {
                    return Ok(new
                    {
                        statusCode = 200,
                        data = get.ToList(),             
                    });
                }

                else
                {
                    return Ok(new
                    {
                        statusCode = 200,
                        data = get.ToList()
                    });

                }
            }
            else
            {
                return Unauthorized(new
                {
                    statusCode = 401,
                    message = "You are not authorized to perfume this operation"
                });
            }
            
        }

        [HttpPost("AddUser")]
        public async Task <IActionResult> Adduser(User user)
        {
            var clientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

            var currentLocation = await _location.GetLocation();

            UserData.Users.Add(user);
            
            return CreatedAtAction(nameof(Adduser), new
            {
                StatusCode = 201,
                user,
                message = "User added sucessfully",
                ipAddress = clientIp,
                location = currentLocation,
            });
        }
    }
}
