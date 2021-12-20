using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Default Profile from C:\Users\Swamy\.aws file
var client = new AmazonDynamoDBClient();
builder.Services.AddSingleton<IAmazonDynamoDB>(client);
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();


//var credentials = new BasicAWSCredentials(builder.Configuration["AWS:AccessKey"]
//    , builder.Configuration["AWS:SecretKey"]);
//var config = new AmazonDynamoDBConfig()
//{
//    RegionEndpoint = RegionEndpoint.USEast2
//};
//var client = new AmazonDynamoDBClient(credentials, config);
//builder.Services.AddSingleton<IAmazonDynamoDB>(client);
//builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
