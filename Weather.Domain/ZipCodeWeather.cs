using System.ComponentModel.DataAnnotations;

namespace Weather.Domain
{
    public class ZipCodeWeather
    {
        [Key]
        public string ZipCode { get; set; }
        public int Temparature { get; set; }
    }
}