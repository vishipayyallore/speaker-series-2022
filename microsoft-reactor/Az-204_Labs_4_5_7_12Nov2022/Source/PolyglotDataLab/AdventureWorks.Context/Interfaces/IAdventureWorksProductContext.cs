using AdventureWorks.Models;

namespace AdventureWorks.Context
{
    public interface IAdventureWorksProductContext
    {
        Task<Model> FindModelAsync(Guid id);

        Task<List<Model>> GetModelsAsync();

        Task<Product> FindProductAsync(Guid id);
    }
}