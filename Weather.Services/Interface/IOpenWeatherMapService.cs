using System.Threading.Tasks;
using Weather.Services.Model;

namespace Weather.Services
{
    /// <summary>
    /// IOpenWeatherMapService
    /// </summary>
    public interface IOpenWeatherMapService
    {
        /// <summary>
        /// GetWeatherData
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public Task<OpenWeatherMapResponse> GetWeatherData(string zipCode);
    }
}
