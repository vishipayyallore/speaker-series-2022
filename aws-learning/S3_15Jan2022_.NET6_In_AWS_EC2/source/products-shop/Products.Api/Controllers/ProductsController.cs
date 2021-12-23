using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiInAws.Data;

namespace WebApiInAws.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly IDynamoDBContext _dynamoDbContext;

        public ProductsController(ILogger<ProductsController> logger, IDynamoDBContext dynamoDbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _dynamoDbContext = dynamoDbContext ?? throw new ArgumentNullException(nameof(dynamoDbContext));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDbModel), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ProductDbModel>> Post([FromBody] ProductDbModel product)
        {
            _logger.LogInformation("Received the ProductsController::Post() request.");

            try
            {
                product.Id = $"{Guid.NewGuid()}";
                await _dynamoDbContext.SaveAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            _logger.LogInformation("Sending output from ProductsController::Post() request.");

            return Created("", product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDbModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDbModel>>> Get()
        {
            _logger.LogInformation("Received the ProductsController::Get() request.");

            // Get the Table ref from the Model
            var table = _dynamoDbContext.GetTargetTable<ProductDbModel>();

            var scanOps = new ScanOperationConfig();

            //if (!string.IsNullOrEmpty(paginationToken))
            //{
            //    scanOps.PaginationToken = paginationToken;
            //}

            // returns the set of Document objects for the supplied ScanOptions
            var results = table.Scan(scanOps);
            List<Document> productsData = await results.GetNextSetAsync();
            IEnumerable<ProductDbModel> products = _dynamoDbContext.FromDocuments<ProductDbModel>(productsData);

            _logger.LogInformation("Sending output from ProductsController::Get() request.");

            return Ok(products);
        }

    }

}
