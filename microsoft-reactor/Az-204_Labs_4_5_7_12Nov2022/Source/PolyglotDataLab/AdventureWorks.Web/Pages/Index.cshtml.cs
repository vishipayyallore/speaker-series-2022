using AdventureWorks.Context;
using AdventureWorks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdventureWorks.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAdventureWorksProductContext _productContext;

        public IndexModel(ILogger<IndexModel> logger, IAdventureWorksProductContext productContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
        }

        [BindProperty(SupportsGet = true)]
        public List<Model> Models { get; set; } = new List<Model>();

        public async Task OnGetAsync()
        {
            this.Models = await _productContext.GetModelsAsync();
        }

    }
}