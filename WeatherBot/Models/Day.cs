using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Models
{
    public class Day
    {
        [JsonProperty("datetime")]
        public DateTime DateOfForecast { get; set; }
        public string TempMin { get; set; }
        public string TempMax { get; set; }
        public string TempFeelsLike { get; set; }
        public string Humidity { get; set; }
        public TimeOnly SunRise { get; set; }

    }
}
