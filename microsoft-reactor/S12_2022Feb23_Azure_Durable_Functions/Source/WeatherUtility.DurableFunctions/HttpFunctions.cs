using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using WeatherUtility.Core.DTOs;

namespace WeatherUtility.DurableFunction
{
    public static class HttpFunctions
    {

        [FunctionName(nameof(WeatherUtilityStarter))]
        public static async Task<IActionResult> WeatherUtilityStarter(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {

            // Retrieve the DTO from Request
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var requestData = JsonConvert.DeserializeObject<WeatherRequestDto>(requestBody);

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("WeatherUtilityOrchestrator", null, requestData);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

    }

}
