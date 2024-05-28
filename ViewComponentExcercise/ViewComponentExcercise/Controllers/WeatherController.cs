using Microsoft.AspNetCore.Mvc;
using ViewComponentExcercise.Models;

namespace ViewComponentExcercise.Controllers
{
    public class WeatherController : Controller
    {
        private List<City> cities = new List<City> {
        new City () { CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime( "2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
        new City () { CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime( "2030-01-01 3:00"),  TemperatureFahrenheit = 60 },
        new City () { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82 }
        };

        [Route("/")]
        public IActionResult Index()
        {
            return View(cities);
        }

        [Route("/weather/{cityCode?}")]
        public IActionResult City(string? cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return View();
            }

            City? city = cities.Where(city => city.CityUniqueCode == cityCode).FirstOrDefault();
            return View(city);
        }
    }
}
