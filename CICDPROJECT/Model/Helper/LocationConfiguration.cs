
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

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
                var locationUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={clientIp}&appid={_openWeatherApiKey}";
                var locationResponse = await httClient.GetStringAsync(locationUrl);
                var locationData = JArray.Parse(locationResponse).FirstOrDefault();

                if (locationData != null)
                {
                    location = locationData["name"].ToString();                   
                }
            }
            return location;
        }
       
    }
}
