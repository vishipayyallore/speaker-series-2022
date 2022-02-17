using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuncApp.HealthCheckDemo
{
    public class SQLDb1HealthCheck
    {

        private readonly HealthCheckService _healthCheckService;

        public SQLDb1HealthCheck(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService ?? throw new ArgumentNullException(nameof(healthCheckService));
        }

        [FunctionName("SQLDb1HealthCheck")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _healthCheckService.CheckHealthAsync((check) => check.Tags.Contains("SQLDatabase1Check"), cancellationToken);

            return new OkObjectResult(result);
        }

    }

}
