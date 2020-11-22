using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.DataAccess;
using Weather.Domain;

namespace Weather.Services
{
    public class DataService : IDataService
    {
        private readonly WeatherContext _context;
        public readonly ILogger<IDataService> _logger;

        public DataService(WeatherContext context, ILogger<IDataService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InsertWeatherData(List<ZipCodeWeather> weathers)
        {
            _logger.LogInformation("Insert Weather Data...");
            foreach (var weather in weathers)
            {
                await _context.ZipCodeWeather.AddAsync(weather);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<WeatherDataResponse> ReadWeatherData()
        {
            _logger.LogInformation("Read Weather Data...");
            
            var response = new WeatherDataResponse();
            
            var result = await _context.ZipCodeWeather.ToListAsync();
            
            response.ZipCodeWeathers = result;
            
            //response.ZipCodeWeathers = new List<ZipCodeWeather>();
            //response.ZipCodeWeathers.Add(new ZipCodeWeather { ZipCode = "98012", Temparature = 50 });
            //response.ZipCodeWeathers.Add(new ZipCodeWeather { ZipCode = "98033", Temparature = 60 });
            //response.ZipCodeWeathers.Add(new ZipCodeWeather { ZipCode = "98006", Temparature = 70 });
            
            return response;
            
        }
    }
}
