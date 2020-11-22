using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Weather.Services
{
    public interface IOpenWeatherMapService
    {
        public Task GetWeatherData();
    }
}
