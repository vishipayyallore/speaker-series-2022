using AdventureWorks.Context;
using AdventureWorks.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));

builder.Services.AddScoped<IAdventureWorksProductContext, AdventureWorksCosmosContext>(
    provider => new
    AdventureWorksCosmosContext(builder.Configuration.GetConnectionString(nameof(AdventureWorksCosmosContext)))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
