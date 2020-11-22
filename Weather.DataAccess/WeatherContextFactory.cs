using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Weather.DataAccess
{
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
                .AddJsonFile("local.settings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            _connectionString = config.GetConnectionStringOrSetting("SqlConnectionString");

            _connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Weather";
        }
    }

}
