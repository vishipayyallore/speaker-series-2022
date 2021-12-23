using Products.Data;

namespace Products.DataServices
{
    public interface IProductsDataServices
    {

        Task<IEnumerable<ProductDbModel>> GetAllBooks();
    }
}
