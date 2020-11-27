using System.Threading.Tasks;
using Weather.Domain;

namespace Weather.Services
{
    /// <summary>
    /// Interface for IDataService
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// InsertWeatherData
        /// </summary>
        /// <param name="weather"></param>
        /// <returns></returns>
        public Task<ZipCodeWeather> InsertWeatherData(ZipCodeWeather weather);

        /// <summary>
        /// ReadWeatherData
        /// </summary>
        /// <returns></returns>
        public Task<WeatherDataResponse> ReadWeatherData();
    }
}
