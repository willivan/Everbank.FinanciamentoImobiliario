using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EverBank.FInanciamentoImobiliario.Contratos.Servico.Controllers
{
    /// <summary>
    /// This controller provides weather forecast data for the EverBank Financiamento Imobiliario system.
    /// It serves as a demo endpoint that returns random weather data for testing and development purposes.
    /// The controller follows RESTful API design patterns and returns data in JSON format.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastChangedController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastChangedController> _logger;

        public WeatherForecastChangedController(ILogger<WeatherForecastChangedController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
