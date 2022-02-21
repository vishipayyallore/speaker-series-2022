using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherUtility.Core.DTOs;
using WeatherUtility.Core.Entities;

namespace WeatherUtility.DurableFunction
{
    public static class OrchestratorFunctions
    {

        [FunctionName(nameof(WeatherUtilityOrchestrator))]
        public static async Task<IList<WeatherData>> WeatherUtilityOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            IList<WeatherData> weatherData;

            var weatherRequestDto = context.GetInput<WeatherRequestDto>();

            // Replace "hello" with the name of your Durable Activity Function.
            weatherData = await context.CallActivityAsync<IList<WeatherData>>("GetWeatherData", weatherRequestDto.Name);

            weatherData = await context.CallActivityAsync<IList<WeatherData>>("GetCelsiusToFahrenheit", weatherData);

            weatherData = await context.CallActivityAsync<IList<WeatherData>>("GetComfortIndex", weatherData);

            // returns Weather Data
            return weatherData;
        }

    }

}