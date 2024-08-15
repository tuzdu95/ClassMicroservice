using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Linq;

namespace ClassApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDistributedCache _cache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var value = await _cache.GetAsync("weather");

            // You might also use !value.IsNullOrEmpty
            if (value is null)
            {
                return Ok(JsonSerializer.Deserialize<object>(value));
            }
            else
            {
                var result = Enumerable.Range(1, 1000).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToList();
                await _cache.SetAsync("weather", JsonSerializer.SerializeToUtf8Bytes(result));
                return Ok(result);
            }
        }
    }
}