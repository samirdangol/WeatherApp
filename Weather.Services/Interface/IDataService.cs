using System.Threading.Tasks;

namespace Weather.Services
{
    public interface IDataService
    {
        public Task InsertWeatherData();
    }
}
