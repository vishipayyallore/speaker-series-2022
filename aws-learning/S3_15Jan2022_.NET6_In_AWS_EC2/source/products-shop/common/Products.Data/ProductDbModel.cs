using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Data
{

    [DynamoDBTable("products")]
    public class ProductDbModel
    {
        [DynamoDBHashKey]
        public string Id { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string PictureUrl { get; set; } = $"/images/books/Book{new Random().Next(1, 10)}.jpg";

        [DynamoDBProperty]
        public string Name { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string Description { get; set; } = string.Empty;

        [DynamoDBProperty]
        public bool IsActive { get; set; } = true;

        [DynamoDBProperty]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 12.56M;

        [DynamoDBProperty]
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        [DynamoDBProperty]
        public DateTime ModifiedDateTime { get; set; } = DateTime.Now;
    }

}