using WeatherUtility.Core.Entities;
using WeatherUtility.Core.Interfaces;

namespace WeatherUtility.Lib.Services
{

    public class WeatherDataService : IWeatherDataService
    {

        // TODO: Get this data from external sources Weather API's OR SQLite/SQL Server/Cosmos etc.
        public IList<WeatherData> GetWeatherDataFromDatabase() => new List<WeatherData>
            {
                new WeatherData { Location = "Bengaluru", TemperatureCelsius = 23, Humidity= 65 },
                new WeatherData { Location = "Hyderabad", TemperatureCelsius = 35, Humidity= 65 },
                new WeatherData { Location= "Kochi", TemperatureCelsius = 19, Humidity = 73 },
                new WeatherData { Location = "Salem", TemperatureCelsius = 23, Humidity= 65 },
                new WeatherData { Location = "Visakhapatnam", TemperatureCelsius = 21, Humidity = 55},
            };

    }

}
