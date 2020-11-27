using System;
using System.ComponentModel.DataAnnotations;

namespace Weather.Domain
{
    public class ZipCodeWeather
    {
        [Key]
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string WeatherDesc { get; set; }
        public double Temp { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public string Wind { get; set; }
        public string Cloud { get; set; }
        public string Pressure { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime AsOfDate { get; set; }
    }
}