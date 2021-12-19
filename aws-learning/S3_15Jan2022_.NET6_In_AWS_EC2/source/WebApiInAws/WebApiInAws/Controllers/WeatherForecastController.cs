using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;

namespace WebApiInAws.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDynamoDBContext _dynamoDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDynamoDBContext dynamoDbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _dynamoDbContext = dynamoDbContext ?? throw new ArgumentNullException(nameof(dynamoDbContext));
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {



            //AmazonDynamoDBConfig clientConfig = new();
            //// This client will access the US East 1 region.
            //clientConfig.RegionEndpoint = RegionEndpoint.USEast2;

            var productNumber = new Random().Next(1, 9999);
            // Temporary Code for DynamoDb
            var product1 = new ProductDbModel()
            {
                Id = $"{Guid.NewGuid()}",
                Name = $"Product {productNumber}",
                Description = $"Product {productNumber} Description",
                CreationDateTime = DateTime.Now
            };

            try
            {
                // _dynamoDbContext.SaveAsync(product1).Wait();
                _dynamoDbContext.SaveAsync<ProductDbModel>(product1).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //using (var dynamoDbClient = new AmazonDynamoDBClient(clientConfig))
            //{
            //    using (var context = new DynamoDBContext(dynamoDbClient))
            //    {
            //        context.SaveAsync(product1);
            //    }
            //}

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}