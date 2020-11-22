using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Domain;

namespace Weather.Services
{
    public interface IDataService
    {
        public Task InsertWeatherData(List<ZipCodeWeather> weathers);

        public Task<WeatherDataResponse> ReadWeatherData();
    }
}
