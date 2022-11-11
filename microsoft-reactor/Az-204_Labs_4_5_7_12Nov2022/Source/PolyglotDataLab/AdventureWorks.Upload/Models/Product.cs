namespace AdventureWorks.Upload.Models
{
    public class Product
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal? Weight { get; set; }
        public decimal ListPrice { get; set; }
        public string Photo { get; set; }
    }
}