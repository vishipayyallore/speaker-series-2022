using Amazon.DynamoDBv2.DataModel;

namespace WebApiInAws.Data
{

    [DynamoDBTable("products")]
    public class ProductDbModel
    {
        [DynamoDBHashKey]
        public string Id { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string Name { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string Description { get; set; } = string.Empty;

        [DynamoDBProperty]
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        [DynamoDBProperty]
        public DateTime ModifiedDateTime { get; set; } = DateTime.Now;
    }

}
