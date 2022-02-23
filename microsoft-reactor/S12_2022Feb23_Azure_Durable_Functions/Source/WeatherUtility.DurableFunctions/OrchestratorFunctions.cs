using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherUtility.Core.DTOs;
using WeatherUtility.Core.Entities;

namespace WeatherUtility.DurableFunction
{
    public static class OrchestratorFunctions
    {

        [FunctionName(nameof(WeatherUtilityOrchestrator))]
        public static async Task<WeatherResponseDto> WeatherUtilityOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            // log = context.CreateReplaySafeLogger(log);

            IList<WeatherData> weatherData;
            WeatherResponseDto weatherResponseDto = new();

            log.LogInformation("Retrieving the DTO from Request");
            var weatherRequestDto = context.GetInput<WeatherRequestDto>();

            try
            {
                log.LogInformation("Invoking GetWeatherData() Activity Function.");
                weatherData = await context.CallActivityAsync<IList<WeatherData>>("GetWeatherData", weatherRequestDto.Name);

                log.LogInformation("Invoking GetCelsiusToFahrenheit() Activity Function.");
                weatherData = await context.CallActivityAsync<IList<WeatherData>>("GetCelsiusToFahrenheit", weatherData);
                // throw new Exception("Just Chill !!");

                log.LogInformation("Invoking GetComfortIndex() Activity Function.");
                weatherData = await context.CallActivityAsync<IList<WeatherData>>("GetComfortIndex", weatherData);

                weatherResponseDto.Data = weatherData;
            }
            catch (Exception error)
            {
                log.LogError(error.Message);

                weatherResponseDto.Success = false;
                weatherResponseDto.Message = "Unable to process. Please speak to the Administrator";
            }

            return weatherResponseDto;
        }

    }

}