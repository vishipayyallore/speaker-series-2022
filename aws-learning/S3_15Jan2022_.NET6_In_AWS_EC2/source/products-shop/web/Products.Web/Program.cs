using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Products.DataServices;
using Products.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IProductsDataServices, ProductsDataServices>(client =>
    {
#pragma warning disable CS8604 // Possible null reference argument.
        client.BaseAddress = new Uri(builder.Configuration["ApiUrls:Products"]);
#pragma warning restore CS8604 // Possible null reference argument.
    }
);

await builder.Build().RunAsync();
