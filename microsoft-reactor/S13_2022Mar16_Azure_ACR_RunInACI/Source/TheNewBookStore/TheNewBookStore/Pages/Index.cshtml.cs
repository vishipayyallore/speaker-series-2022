using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TheNewBookStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void OnGet()
        {
            ConnectionString = _configuration["MongoDbSettings:ConnectionString"];
        }
    }
}