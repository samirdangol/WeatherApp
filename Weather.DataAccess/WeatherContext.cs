using Microsoft.EntityFrameworkCore;
using Weather.Domain;

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
