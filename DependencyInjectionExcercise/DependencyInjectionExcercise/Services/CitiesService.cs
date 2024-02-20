using DependencyInjectionExcercise.Models;
using ServiceContracts;

namespace Services
{
    public class CitiesService : ICitiesService
    {
        private readonly List<City> _cities;

        public CitiesService()
        {
            _cities = new List<City>()
            {
                new City () { CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime( "2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
                new City () { CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime( "2030-01-01 3:00"),  TemperatureFahrenheit = 60 },
                new City () { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82 }
            };
        }

        public List<City> GetDetails()
        {
            return _cities;
        }

        public City? GetWeatherByCityCode(string cityCode)
        {
            City? city = _cities.FirstOrDefault(x => x.CityUniqueCode == cityCode);
            return city;
        }
    }
}

