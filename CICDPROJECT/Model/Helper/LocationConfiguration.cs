
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
                var locationData = JObject.Parse(locationResponse);


                if (locationData !=null)
                {
                    var city = locationData["city"]?.ToString() ?? "unknown";
                    var country = locationData["country"]?.ToString() ?? "unknown";

                    location = (city == "unknown" && country == "unknown") ? "unknown" : $"{city}, {country}";
                }
            }
            return location;
        }
       
    }
}
