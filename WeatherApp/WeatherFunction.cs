using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
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

        [FunctionName("write")]
        public async Task<IActionResult> FetchAndStore(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Starting write function...");

            // Read request 
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var zipCode = JsonConvert.DeserializeObject<string>(requestBody);

            if(string.IsNullOrWhiteSpace(zipCode))
            {
                return new BadRequestObjectResult("Invalid ZipCode");
            }

            // Fetch Data from open weather map api
            var res = await _weatherMapService.GetWeatherData(zipCode);

            if(res == null)
            {
                return new BadRequestObjectResult("Fail to retrieve weather data");
            }

            // Map to Entity
            var weather = new ZipCodeWeather
            {
                ZipCode = zipCode,
                City = res.Name,
                Country = res.Sys.Country,
                WeatherDesc = res.Weather.FirstOrDefault().Description,
                Temp = res.Main.Temp,
                TempMin = res.Main.Temp_min,
                TempMax = res.Main.Temp_max,
                Wind = $"{res.Wind.Speed}mph {DegreeToDirectoin(res.Wind.Deg)}",
                Cloud = $"{res.Clouds.All}%",
                Pressure = $"{res.Main.Pressure}hpa",
                Longitude = res.Coord.Lon,
                Latitude = res.Coord.Lat,
                AsOfDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(res.Dt).ToLocalTime()
            };

            // Insert into Database
            var response = await _dataService.InsertWeatherData(weather);
            
            return new OkObjectResult(response);
        }

        [FunctionName("read")]
        public async Task<IActionResult> Read(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Starting read function...");
            
            var response = await _dataService.ReadWeatherData();
            return new OkObjectResult(response);
        }

        private string DegreeToDirectoin(int deg)
        {
            if (deg >= 0 && deg < 45) return "N";
            if (deg >= 45 && deg < 90) return "NE";
            if (deg >= 90 && deg < 135) return "E";
            if (deg >= 135 && deg < 180) return "SE";
            if (deg >= 180 && deg < 225) return "S";
            if (deg >= 225 && deg < 270) return "SW";
            if (deg >= 270 && deg < 315) return "W";
            if (deg >= 315 && deg < 360) return "NW";
            return string.Empty;
        }
    }
}
