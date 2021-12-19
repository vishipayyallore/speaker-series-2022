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

//var awsOptions = builder.Configuration.GetAWSOptions();
//builder.Services.AddDefaultAWSOptions(awsOptions);
//var dynamoDbConfig = new AmazonDynamoDBConfig()
//{
//    RegionEndpoint = RegionEndpoint.USEast2
//};
//var client = new AmazonDynamoDBClient(dynamoDbConfig);

var credentials = new BasicAWSCredentials("YourKey", "YourSecretKey");
var config = new AmazonDynamoDBConfig()
{
    RegionEndpoint = RegionEndpoint.USEast2
};
var client = new AmazonDynamoDBClient(credentials, config);
builder.Services.AddSingleton<IAmazonDynamoDB>(client);
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

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
