using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Models
{
    public class DayJson
    {
        [JsonProperty("days")]
        public List<Day> Day { get; set; }
    }
}
