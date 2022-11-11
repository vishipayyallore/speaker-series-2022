using AdventureWorks.Context;
using AdventureWorks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IAdventureWorksProductContext _productContext;

        public DetailsModel(IAdventureWorksProductContext productContext)
        {
            _productContext = productContext;
        }

        [BindProperty(SupportsGet = true)]
        public Model Model { get; set; } = new();

        [BindProperty, Required(ErrorMessage = "Please select a product.")]
        public string SelectedProductId { get; set; } = String.Empty;

        public async Task OnGetAsync(Guid id)
        {
            this.Model = await _productContext.FindModelAsync(id);
        }

    }
}
