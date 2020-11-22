using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Weather.DataAccess
{
    public class WeatherContextFactory : IDesignTimeDbContextFactory<WeatherContext>
    {
        public WeatherContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeatherContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=StoreDB1;");

            return new WeatherContext(optionsBuilder.Options);
        }
    }

}
