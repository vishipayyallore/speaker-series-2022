using HealthCheckUIDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string baseURL = "http://localhost:7071/";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseURL) });

builder.Services.AddHttpClient<ISQLDbHealthCheckService, SQLDbHealthCheckService>(client =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    client.BaseAddress = new Uri(baseURL);
#pragma warning restore CS8604 // Possible null reference argument.
}
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
