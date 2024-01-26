using Microsoft.AspNetCore.Mvc;
using RazorViewsExcercise.Models;

namespace RazorViewsExcercise.Controllers
{
    public class WeatherController : Controller
    {
        private List<CityWeather> cities = new List<CityWeather>()
        {
            new CityWeather() { CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
            new CityWeather() {CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60},
            new CityWeather() {CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82}

        };

        [Route("/")]
        public IActionResult Index()
        {
            return View(cities);
        }

        [Route("/weather/{cityCode?}")]
        
        public IActionResult City(string? cityCode)
        {
            if (string.IsNullOrWhiteSpace(cityCode))
            {
                return View();
            }

            CityWeather? city = cities.Where(c =>  c.CityUniqueCode == cityCode).FirstOrDefault();

            return View(city);
        }
    }
}
