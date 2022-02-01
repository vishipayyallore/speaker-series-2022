using System.Net.Http.Headers;

namespace WebAPIApp.BenchmarkDemo
{
    public class BooksStoreApp
    {

        private static readonly HttpClient _client = new();
        private static readonly string _url = "https://app-books-store-basic.azurewebsites.net/Books";
        // private static readonly string _url = "https://app-books-store-shared.azurewebsites.net/Books";

        public async Task<string> GetAllBooksAsync()
        {
            string professors = string.Empty;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await _client
                                                        .GetAsync(_url)
                                                        .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    professors = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            return professors;
        }

    }
}
