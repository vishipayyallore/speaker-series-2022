using FuncApp.HealthCheckDemo;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FuncApp.HealthCheckDemo
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .AddCheck<SampleHealthCheck>("SQLDatabase1Check",
                HealthStatus.Unhealthy,
                tags: new[] { "SQLDatabase1Check" });
        }
    }

}
