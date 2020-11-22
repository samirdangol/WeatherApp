using System.Threading.Tasks;
using Weather.DataAccess;
using Weather.DataAccess.Model;

namespace Weather.Services
{
    public class DataService : IDataService
    {
        private readonly WeatherContext _context;

        public DataService(WeatherContext context)
        {
            _context = context;
        }

        public async Task InsertWeatherData()
        {
            var weather = new ZipCodeWeather();
            await _context.ZipCodeWeather.AddAsync(weather);
            await _context.SaveChangesAsync();
        }
    }
}
