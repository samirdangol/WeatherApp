using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
            log.LogInformation("asdf");

            // Read request 
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var zipCodes = JsonConvert.DeserializeObject<List<string>>(requestBody);
            string responseMessage = string.Empty;
            if (zipCodes != null)
            {
                foreach(var zip in zipCodes)
                {
                    responseMessage += $"{zip} ";
                }
            }
           
            // Fetch Data from Weather API
            await _weatherMapService.GetWeatherData();

            // Insert into Database
            await _dataService.InsertWeatherData();
            
            return new OkObjectResult(responseMessage);
        }
    }
}
