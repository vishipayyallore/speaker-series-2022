namespace HealthCheckUIDemo.Services
{
    public interface ISQLDbHealthCheckService
    {
        Task<dynamic> GetSQLHealthCheck();
    }
}