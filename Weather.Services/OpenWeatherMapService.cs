using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Weather.Services
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly HttpClient _client;
        private readonly ILogger<IOpenWeatherMapService> _logger;

        public OpenWeatherMapService(HttpClient client, ILogger<IOpenWeatherMapService> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task GetWeatherData()
        {
            _logger.LogInformation("Hello GetWeatherData");
            var response = await _client.GetAsync("https://api.openweathermap.org/data/2.5/weather?zip=98012&units=imperial&appid=8358673f01a549b46b006ef9858d324e");
            
        }
    }
}
