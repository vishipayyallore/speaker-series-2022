using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FuncApp.HealthCheckDemo
{
    public class SQLDb1HealthCheck
    {

        private readonly SampleHealthCheck _sampleHealthCheck;
        private readonly HealthCheckService _healthCheckService;

        public SQLDb1HealthCheck(SampleHealthCheck sampleHealthCheck, HealthCheckService healthCheckService)
        {
            _sampleHealthCheck = sampleHealthCheck ?? throw new ArgumentNullException(nameof(sampleHealthCheck));

            _healthCheckService = healthCheckService ?? throw new ArgumentNullException(nameof(healthCheckService));
        }

        [FunctionName("SQLDb1HealthCheck")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //var result = await _sampleHealthCheck.CheckHealthAsync(new HealthCheckContext(), cancellationToken);
            //return new OkObjectResult(result);

            var result = await _healthCheckService.CheckHealthAsync((check) => check.Tags.Contains("SQLDatabase1Check"), cancellationToken);
            return new OkObjectResult(result);

            // return new OkObjectResult(responseMessage);
        }
    }
}
