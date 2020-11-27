using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public async Task<ZipCodeWeather> InsertWeatherData(ZipCodeWeather weather)
        {
            _logger.LogInformation("Insert Weather Data...");

            var result = await _context.ZipCodeWeather.FindAsync(weather.ZipCode);
            ZipCodeWeather response;

            if(result != null)
            {
                // update
                result.City = weather.City;
                result.Country = weather.Country;
                result.WeatherDesc = weather.WeatherDesc;
                result.Temp = weather.Temp;
                result.TempMin = weather.TempMin;
                result.TempMax = weather.TempMax;
                result.Wind = weather.Wind;
                result.Cloud = weather.Cloud;
                result.Pressure = weather.Pressure;
                result.Longitude = weather.Longitude;
                result.Latitude = weather.Latitude;
                result.AsOfDate = weather.AsOfDate;
                response = result;
            }
            else
            {
                // insert
                var entityEntry = await _context.ZipCodeWeather.AddAsync(weather);
                response = entityEntry.Entity;
            }

            var recordCount = await _context.SaveChangesAsync();
            _logger.LogInformation($"{recordCount} records added or updated successfully.");
            return response;
        }

        public async Task<WeatherDataResponse> ReadWeatherData()
        {
            _logger.LogInformation("Read Weather Data...");
            
            var response = new WeatherDataResponse();
            
            var result = await _context.ZipCodeWeather.ToListAsync();
            
            response.ZipCodeWeathers = result;
            
            return response;
            
        }
    }
}
