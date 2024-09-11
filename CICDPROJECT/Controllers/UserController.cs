using CICDPROJECT.Model;
using CICDPROJECT.Model.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

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
        public UserController(LocationConfiguration location, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IOptions<OpenWeatherOption> openWeatherOptions)
        {
            _httpClientFactory = httpClientFactory;
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
            var location = "unknown";
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

            using (var httClient = _httpClientFactory.CreateClient())
            {
                var locationUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={clientIp}&appid={_openWeatherApiKey}";
                var locationResponse = await httClient.GetStringAsync(locationUrl);
                var locationData = JArray.Parse(locationResponse).FirstOrDefault();

                if (locationData != null)
                {
                    location = locationData["name"].ToString();
                 
                }

            }

            UserData.Users.Add(user);
            
            return CreatedAtAction(nameof(Adduser), new
            {
                StatusCode = 201,
                user,
                message = "User added sucessfully",
                ipAddress = clientIp,
                UserLocation = location,
            });
        }
    }
}
