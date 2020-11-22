using System.Threading.Tasks;

namespace Weather.Services
{
    public interface IOpenWeatherMapService
    {
        public Task<OpenWeatherMapResponse> GetWeatherData(string zipCode);
    }
}
