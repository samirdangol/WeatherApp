using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Weather.Domain;
using Weather.Services;

namespace WeatherApp
{
    public class WeatherFunction
    {
        
        private readonly IOpenWeatherMapService _weatherMapService;
        private readonly IDataService _dataService;

        public WeatherFunction(IOpenWeatherMapService weatherMapService, IDataService dataService)
        {
            _weatherMapService = weatherMapService;
            _dataService = dataService;
        }

        [FunctionName("WeatherFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Starting function...");

            // Read request 
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var zipCodes = JsonConvert.DeserializeObject<List<string>>(requestBody);

            var weathers = new List<ZipCodeWeather>();

            // Fetch Data from open weather map api
            foreach (var zipCode in zipCodes)
            {
                var openWeatherMapResponse = await _weatherMapService.GetWeatherData(zipCode);
                weathers.Add(new ZipCodeWeather { ZipCode = zipCode, Temparature = (int)openWeatherMapResponse.Main.Temp });
            }

            // Insert into Database
            await _dataService.InsertWeatherData(weathers);

            var response = await _dataService.ReadWeatherData();
            
            return new OkObjectResult(response);
        }
    }
}
