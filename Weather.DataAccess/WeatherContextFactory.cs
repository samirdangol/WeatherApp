using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Weather.DataAccess
{
    /// <summary>
    /// WeatherContextFactory class for migration
    /// </summary>
    public class WeatherContextFactory : IDesignTimeDbContextFactory<WeatherContext>
    {
        private static string _connectionString;
        
        public WeatherContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var optionsBuilder = new DbContextOptionsBuilder<WeatherContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            return new WeatherContext(optionsBuilder.Options);
        }

        private static void LoadConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.prod.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            _connectionString = config.GetConnectionStringOrSetting("SqlConnectionString");
        }
    }

}
