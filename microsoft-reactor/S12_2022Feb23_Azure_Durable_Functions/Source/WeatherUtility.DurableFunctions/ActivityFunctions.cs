using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherUtility.Core.Entities;

namespace WeatherUtility.DurableFunction
{

    public static class ActivityFunctions
    {

        [FunctionName(nameof(GetWeatherData))]
        public static async Task<IList<WeatherData>> GetWeatherData([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Request received at ActivityFunctions::GetWeatherData() {DateTime.UtcNow} for user {name}.");

            return await Task.FromResult(GetWeatherDataFromDatabase());
        }

        [FunctionName(nameof(GetCelsiusToFahrenheit))]
        public static async Task<IList<WeatherData>> GetCelsiusToFahrenheit([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Request received at ActivityFunctions::GetCelsiusToFahrenheit() {DateTime.UtcNow} for user {name}.");

            return await Task.FromResult(GetWeatherDataFromDatabase());
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
