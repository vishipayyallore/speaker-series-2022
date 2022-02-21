using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            // Replace "hello" with the name of your Durable Activity Function.
            weatherData = await context.CallActivityAsync<IList<WeatherData>>("GetWeatherData", "SriVaru");

            weatherData = await context.CallActivityAsync<IList<WeatherData>>("GetCelsiusToFahrenheit", weatherData);

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return weatherData;
        }

    }

}