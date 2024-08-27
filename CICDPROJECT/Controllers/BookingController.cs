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

        [HttpGet("testing")]
        public IActionResult Get()
        {
            List<int> numbers = new List<int>();

            for (int i = 1; i <= 50; i++)
            {
                numbers.Add(i);
            }

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
            return Ok(numbers);
        }

    }
}
