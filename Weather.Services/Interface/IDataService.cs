using System.Threading.Tasks;
using Weather.Domain;

namespace Weather.Services
{
    public interface IDataService
    {
        public Task InsertWeatherData();

        public Task<WeatherDataResponse> ReadWeatherData();
    }
}
