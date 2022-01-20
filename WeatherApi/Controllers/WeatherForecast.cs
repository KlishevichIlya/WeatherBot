using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecast : Controller
    {
        private readonly HttpClient client;
        public WeatherForecast()
        {
            client = new HttpClient();
        }

        [HttpGet("forecast")]
        public async Task<string> GetForecastAsync([FromQuery]double latitude, double longitude)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{latitude},{longitude}?key=AE6D3SXYY5DABUW8YZYJV33X7");
            var response = await client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
