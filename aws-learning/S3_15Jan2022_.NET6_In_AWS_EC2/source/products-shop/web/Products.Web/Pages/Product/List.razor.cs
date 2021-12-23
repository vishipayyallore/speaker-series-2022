using Microsoft.AspNetCore.Components;
using Products.Data;
using Products.DataServices;

namespace Products.Web.Pages.Product
{

    public partial class List
    {

        [Inject]
        private IProductsDataServices? ProductsDataService { get; set; }

        public IEnumerable<ProductDbModel>? Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Products = await ProductsDataService.GetAllBooks();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

    }

}
