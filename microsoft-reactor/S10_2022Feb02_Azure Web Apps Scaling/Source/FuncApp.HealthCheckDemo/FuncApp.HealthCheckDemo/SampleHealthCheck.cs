using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace FuncApp.HealthCheckDemo
{

    public class SampleHealthCheck : IHealthCheck
    {

        const string ConnectionString = "YourSQLConnectionString";

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;
            string errorMessage = string.Empty;

            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                }
                catch (DbException ex)
                {
                    isHealthy = false;
                    errorMessage = ex.Message;
                    // return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
                }
            }

            if (isHealthy)
            {
                return HealthCheckResult.Healthy("Connected to SQL Server.");
            }

            return HealthCheckResult.Unhealthy(errorMessage);
        }
    }
}
