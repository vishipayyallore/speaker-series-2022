using Products.Data;

namespace Products.DataServices
{
    public interface IProductsDataServices
    {

        Task<IEnumerable<WeatherForecast>> VerifyApi();

        Task<IEnumerable<ProductDbModel>> GetAllBooks();
    }
}
