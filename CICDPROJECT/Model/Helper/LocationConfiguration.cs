
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace CICDPROJECT.Model.Helper
{
    public class LocationConfiguration
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _openWeatherApiKey;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LocationConfiguration(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherOption> openWeatherOptions, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _openWeatherApiKey = openWeatherOptions.Value.ApiKey;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task <string> GetLocation()
        {
            var location = "unknown";
            var clientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

            using (var httClient = _httpClientFactory.CreateClient())
            {
                var query = $"http://ip-api.com/json/{clientIp}";
                var locationResponse = await httClient.GetStringAsync(query);
                var locationData = JsonDocument.Parse(locationResponse).RootElement;


                if (locationData.TryGetProperty("lat", out var latElement) && locationData.TryGetProperty("lon", out var lonElement))
                {
                    var lat = latElement.GetDouble();
                    var lon = lonElement.GetDouble();
                    location = locationData.GetProperty("city").GetString() ?? "Unknown";

                    var weatherUrl = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={_openWeatherApiKey}";
                    var weatherResponse = await httClient.GetStringAsync(weatherUrl);
                    var weatherData = JObject.Parse(weatherResponse);
                }
            }
            return location;
        }
       
    }
}
