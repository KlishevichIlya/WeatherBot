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
        public async Task<IActionResult> GetForecast()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                " https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Paris?key=AE6D3SXYY5DABUW8YZYJV33X7");
            var response = await client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            return Ok(response);
        }
    }
}
