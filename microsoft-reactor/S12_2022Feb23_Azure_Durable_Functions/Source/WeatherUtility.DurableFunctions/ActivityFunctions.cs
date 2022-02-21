using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherUtility.Core.Entities;
using WeatherUtility.Core.Interfaces;

namespace WeatherUtility.DurableFunction
{

    public class ActivityFunctions
    {

        private readonly ITemperatureConvertor _temperatureConvertor;

        public ActivityFunctions(ITemperatureConvertor temperatureConvertor)
        {
            _temperatureConvertor = temperatureConvertor ?? throw new ArgumentNullException(nameof(temperatureConvertor)); ;
        }

        [FunctionName(nameof(GetWeatherData))]
        public async Task<IList<WeatherData>> GetWeatherData([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Request received at ActivityFunctions::GetWeatherData() {DateTime.UtcNow} for user {name}.");

            return await Task.FromResult(GetWeatherDataFromDatabase());
        }

        [FunctionName(nameof(GetCelsiusToFahrenheit))]
        public async Task<IList<WeatherData>> GetCelsiusToFahrenheit([ActivityTrigger] IList<WeatherData> weatherDatas, ILogger log)
        {
            log.LogInformation($"Request received at ActivityFunctions::GetCelsiusToFahrenheit() {DateTime.UtcNow} for user.");

            foreach (WeatherData weatherData in weatherDatas)
            {
                weatherData.TemperatureFahrenheit = _temperatureConvertor.CelsiusToFahrenheit(weatherData.TemperatureCelsius);
            }

            return await Task.FromResult(weatherDatas);
        }

        // 

        // TODO: Get this data from SQLite/SQL Server/Cosmos etc.
        private static IList<WeatherData> GetWeatherDataFromDatabase() => new List<WeatherData>
            {
                new WeatherData { Location= "San Francisco", TemperatureCelsius = 19, Humidity = 73 },
                new WeatherData { Location = "Denver", TemperatureCelsius = 21, Humidity = 55},
                new WeatherData { Location = "Bologna", TemperatureCelsius = 23, Humidity= 65 },
                new WeatherData { Location = "Hyderabad", TemperatureCelsius = 35, Humidity= 65 }
            };
    }

}
