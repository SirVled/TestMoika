using Microsoft.AspNetCore.Mvc;
using TestMoika.Data;
using TestMoika.Entites;

namespace TestMoika.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly Context _context;

        public WeatherForecastController(Context context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            string ss = string.Empty;
            foreach(var el in _context.SalesPoints.Select(x => x.ProvidedProducts).ToList())
            {
                ss += el.Count + "  ";
            }
            return ss;
            
        }
    }
}