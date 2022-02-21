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

        private readonly IWeatherDataService _weatherDataService;
        private readonly ITemperatureConvertor _temperatureConvertor;
        private readonly ITemperatureUtility _temperatureUtility;

        public ActivityFunctions(ITemperatureConvertor temperatureConvertor, IWeatherDataService weatherDataService, ITemperatureUtility temperatureUtility)
        {
            _weatherDataService = weatherDataService ?? throw new ArgumentNullException(nameof(weatherDataService));

            _temperatureConvertor = temperatureConvertor ?? throw new ArgumentNullException(nameof(temperatureConvertor));

            _temperatureUtility = temperatureUtility ?? throw new ArgumentNullException(nameof(temperatureUtility));
        }

        [FunctionName(nameof(GetWeatherData))]
        public async Task<IList<WeatherData>> GetWeatherData([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Request received at ActivityFunctions::GetWeatherData() {DateTime.UtcNow} for user {name}.");

            return await Task.FromResult(_weatherDataService.GetWeatherDataFromDatabase());
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

        [FunctionName(nameof(GetComfortIndex))]
        public async Task<IList<WeatherData>> GetComfortIndex([ActivityTrigger] IList<WeatherData> weatherDatas, ILogger log)
        {
            log.LogInformation($"Request received at ActivityFunctions::GetComfortIndex() {DateTime.UtcNow} for user.");

            foreach (WeatherData weatherData in weatherDatas)
            {
                weatherData.ComfortIndex = _temperatureUtility.GetComfortIndex(weatherData.TemperatureFahrenheit, weatherData.Humidity);
            }

            return await Task.FromResult(weatherDatas);
        }

    }

}
