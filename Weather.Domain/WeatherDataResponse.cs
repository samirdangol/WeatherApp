using System.Collections.Generic;

namespace Weather.Domain
{
    public class WeatherDataResponse
    {
        public List<ZipCodeWeather> ZipCodeWeathers { get; set; }
    }
}