using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace FuncApp.HealthCheckDemo
{

    public class SampleHealthCheck : IHealthCheck
    {

        string ConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:ConnectionString");

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;
            // string errorMessage = string.Empty;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {

                    await connection.OpenAsync(cancellationToken);

                }
            }
            catch (DbException ex)
            {
                isHealthy = false;
                // errorMessage = ex.Message;
                // return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
            }

            return (isHealthy) ? HealthCheckResult.Healthy("Connected to SQL Server.")
             : HealthCheckResult.Unhealthy("Could Not be Connected to SQL Server.");
        }
    }
}
