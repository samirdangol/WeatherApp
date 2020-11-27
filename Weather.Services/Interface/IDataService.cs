using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Domain;

namespace Weather.Services
{
    public interface IDataService
    {
        public Task<ZipCodeWeather> InsertWeatherData(ZipCodeWeather weather);

        public Task<WeatherDataResponse> ReadWeatherData();
    }
}
