using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Trip_Planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("forecast")]
        public async Task<IActionResult> GetWeatherForecast(string location, int days)
        {
            var apiKey = "86dc608358604267a1691655251202";
            var url = $"http://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={location}&days={days}&aqi=no&alerts=no";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var forecastData = await response.Content.ReadAsStringAsync();
                return Ok(forecastData);
            }

            return BadRequest("Unable to fetch weather forecast.");
        }
    }
}
