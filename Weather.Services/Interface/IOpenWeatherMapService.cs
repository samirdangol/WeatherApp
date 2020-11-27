using System.Threading.Tasks;
using Weather.Services.Model;

namespace Weather.Services
{
    public interface IOpenWeatherMapService
    {
        public Task<OpenWeatherMapResponse> GetWeatherData(string zipCode);
    }
}
