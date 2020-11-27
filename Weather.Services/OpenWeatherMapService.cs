using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Services.Model;

namespace Weather.Services
{
    /// <summary>
    /// Service to Fetch weather details from OpenWeatherMap API
    /// </summary>
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly HttpClient _client;
        private readonly ILogger<IOpenWeatherMapService> _logger;
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public OpenWeatherMapService(HttpClient client, ILogger<IOpenWeatherMapService> logger, IConfiguration config)
        {
            _client = client;
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// GetWeatherData
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public async Task<OpenWeatherMapResponse> GetWeatherData(string zipCode)
        {
            _logger.LogInformation("GetWeatherData from external API");

            var appId = _config.GetConnectionStringOrSetting("OpenWeatherMapAppId");
            
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = "api.openweathermap.org",
                Path = "data/2.5/weather",
                Query = $"zip={zipCode}&units=imperial&appid={appId}"
            };
            
            var response = await _client.GetAsync(uriBuilder.Uri);
            if(!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Failed to load weather data.");
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver 
                { 
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            };
            var openWeatherMapResponse = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(result, settings);

            return openWeatherMapResponse;
        }
    }
}
