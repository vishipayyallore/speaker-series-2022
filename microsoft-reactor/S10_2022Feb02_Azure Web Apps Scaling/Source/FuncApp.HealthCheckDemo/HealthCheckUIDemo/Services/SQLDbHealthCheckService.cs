namespace HealthCheckUIDemo.Services
{
    public class SQLDbHealthCheckService : ISQLDbHealthCheckService
    {
        private readonly HttpClient _httpClient;
        const string apiEndPoint = "api/SQLDb1HealthCheck";

        public SQLDbHealthCheckService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<dynamic> GetSQLHealthCheck()
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var output = await _httpClient.GetFromJsonAsync<dynamic>($"{apiEndPoint}");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            return output;
        }


    }
}
