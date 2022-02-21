using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherUtility.Core.Entities;

namespace WeatherUtility_DurableFunction
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

            // outputs.Add(await context.CallActivityAsync<string>("Function1_Hello", "Seattle"));
            // outputs.Add(await context.CallActivityAsync<string>("Function1_Hello", "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return weatherData;
        }

    }

}