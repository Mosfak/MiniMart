namespace MiniMart.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
