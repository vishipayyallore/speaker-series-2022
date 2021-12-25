using Products.Data;
using System.Net.Http.Json;

namespace Products.DataServices
{

    public class ProductsDataServices : IProductsDataServices
    {
        private readonly HttpClient _httpClient;
        const string productsEndPoint = "api/v1/products";

        public ProductsDataServices(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<ProductDbModel>> GetAllBooks()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductDbModel>>($"{productsEndPoint}");
#pragma warning restore CS8603 // Possible null reference return.
        }


        public async Task<IEnumerable<WeatherForecast>> VerifyApi()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/api/v1/WeatherForecast");
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

}