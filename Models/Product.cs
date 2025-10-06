namespace MiniMart.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? category { get; set; } 
    }
}
