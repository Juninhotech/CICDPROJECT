using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CICDPROJECT.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        [HttpPost]
        public IActionResult Booking(string name)
        {
            return CreatedAtAction(nameof(Booking), new 
            {
                message = $"Hello, {name}, your booking time is: {DateTime.Now}"
            });
        }
    }
}
