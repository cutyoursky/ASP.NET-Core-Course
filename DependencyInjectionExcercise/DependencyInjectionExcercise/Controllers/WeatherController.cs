using DependencyInjectionExcercise.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace DependencyInjectionExcercise.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ICitiesService _citiesService;

        public WeatherController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var cities = _citiesService.GetDetails();
            return View(cities);
        }

        [Route("/weather/{cityCode?}")]
        public IActionResult City(string? cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return View();
            }

            City? city = _citiesService.GetWeatherByCityCode(cityCode);
            return View(city);
        }
    }
}
