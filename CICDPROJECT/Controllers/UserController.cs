using CICDPROJECT.Model;
using CICDPROJECT.Model.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CICDPROJECT.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        LocationConfiguration _location;

        public UserController(LocationConfiguration location)
        {
            _location = location;
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
            var currentLocation = await _location.GetLocation();

            UserData.Users.Add(user);
            
            return CreatedAtAction(nameof(Adduser), new
            {
                StatusCode = 201,
                user,
                message = "User added sucessfully",
                location = currentLocation,
            });
        }


        [HttpGet("convert/{number}")]
        public IActionResult ConvertNumberToWords(int number)
        {
            string result = NumberToWordsConverter.ConvertToWords(number);
            return Ok(new { Number = number, Words = result });
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] Login request)
        {
            var userAgent = Request.Headers["User-Agent"].ToString();
            var deviceInfo = GetDeviceInfo(userAgent);

            return Ok(new
            {
                Message = "Login successful",
                Device = deviceInfo.DeviceName,
                Model = deviceInfo.Model
            });
        }


        private DeviceInfo GetDeviceInfo(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return new DeviceInfo { DeviceName = "Unknown", Model = "Unknown" };

            var regex = new Regex(@"\(([^)]+)\)");
            var match = regex.Match(userAgent);

            if (match.Success)
            {
                string deviceDetails = match.Groups[1].Value;
                return new DeviceInfo { DeviceName = "Mobile", Model = deviceDetails };
            }

            return new DeviceInfo { DeviceName = "Unknown", Model = "Unknown" };
        }
    }
}
