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

        public async Task InsertWeatherData()
        {
            var weather = new ZipCodeWeather();
            await _context.ZipCodeWeather.AddAsync(weather);
            await _context.SaveChangesAsync();
        }

        public async Task<WeatherDataResponse> ReadWeatherData()
        {
            var response = new WeatherDataResponse();
            var result = await _context.ZipCodeWeather.ToListAsync();

            response.ZipCodeWeathers = new List<ZipCodeWeather>();
            response.ZipCodeWeathers.Add(new ZipCodeWeather { ZipCode = "98012", Temparature = 50 });
            response.ZipCodeWeathers.Add(new ZipCodeWeather { ZipCode = "98033", Temparature = 60 });
            response.ZipCodeWeathers.Add(new ZipCodeWeather { ZipCode = "98006", Temparature = 70 });
            response.ZipCodeWeathers = result;
            return response;
            
        }
    }
}
