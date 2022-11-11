namespace AdventureWorks.Upload.Models
{
    public class Model
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public IList<Product> Products { get; set; }
    }
}