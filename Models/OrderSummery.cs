

using System.ComponentModel.DataAnnotations;

namespace MiniMart.Models
{
    public class OrderSummary
    {
        [Key]
        public int OrderSummeryId { get; set; }
        public required int UserId { get; set; }   // to track which customer placed it
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        // Navigation property for related items
        public required ICollection<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        [Key] 
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
