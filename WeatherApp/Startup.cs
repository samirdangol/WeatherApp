﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Weather.DataAccess;
using Weather.Services;

[assembly: FunctionsStartup(typeof(WeatherApp.Startup))]

namespace WeatherApp
{
    /// <summary>
    /// Startup Class
    /// </summary>
    class Startup: FunctionsStartup
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddHttpClient();
            builder.Services.AddLogging();
            builder.Services.AddTransient<IDataService, DataService>();
            builder.Services.AddTransient<IOpenWeatherMapService, OpenWeatherMapService>();

            string SqlConnection = config.GetConnectionStringOrSetting("SqlConnectionString");
            builder.Services.AddDbContext<WeatherContext>(
                options => options.UseSqlServer(SqlConnection));
        }
    }
}
