using DependencyInjectionExcercise.Models;

namespace ServiceContracts
{
    public interface ICitiesService
    {
        List<City> GetDetails();
        City? GetWeatherByCityCode(string cityCode);
    }
}