using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace WebAPIApp.BenchmarkDemo
{

    [SimpleJob(RunStrategy.ColdStart, launchCount: 10, warmupCount: 10, targetCount: 100)]
    public class BenchmarkWebAPIAndApp
    {

        [Params(10, 100)]
        public int IterationCount;

        readonly ProfessorsAPIClient professorsClient = new();
        readonly ProductsAPIClient productsClient = new();
        readonly BooksStoreApp booksStoreApp = new();

        [Benchmark]
        public async Task AppGetAllBooksAsync()
        {
            await booksStoreApp.GetAllBooksAsync().ConfigureAwait(false);
        }

        [Benchmark]
        public async Task ApiGetAllProfessorsAsync()
        {
            await professorsClient.GetAllProfessorsAsync().ConfigureAwait(false);
        }

        [Benchmark]
        public async Task ApiGetAllProductsAsync()
        {
            await productsClient.GetAllProductsAsync().ConfigureAwait(false);
        }
    }

}
