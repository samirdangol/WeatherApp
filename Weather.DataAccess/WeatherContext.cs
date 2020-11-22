using Weather.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Weather.DataAccess
{
    public class WeatherContext: DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
                : base(options)
        { }

        public DbSet<ZipCodeWeather> ZipCodeWeather { get; set; }
    }
}
