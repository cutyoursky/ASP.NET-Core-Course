using DependencyInjectionExcercise.Models;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionExcercise.ViewComponents
{
    public class CityViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(City city)
        {
            ViewBag.Color = GetColorByTemperature(city.TemperatureFahrenheit);

            return View(city);
        }

        string GetColorByTemperature(int temperature)
        {
            if (temperature < 44)
            {
                return "blue-background";
            }
            else if (temperature >= 44 && temperature <= 75)
            {
                return "green-background";
            }
            else
            {
                return "orange-background";
            }
        }
    }
}
