using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniMart.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // In-memory product list
        private static List<Models.Product> _products = new List<Models.Product>
        {
            new Models.Product { Name = "Apple", Price = 0.5M, category = "Fruits" },
            new Models.Product { Name = "Banana", Price = 0.3M, category = "Fruits" },
            new Models.Product { Name = "Carrot", Price = 0.2M, category = "Vegetables" }
        };

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_products);
        }
        [HttpPost]
        public IActionResult AddProduct([FromBody] Models.Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.Name) || product.Price <= 0)
            {
                return BadRequest("Invalid product data.");
            }
            _products.Add(product);
            return CreatedAtAction(nameof(GetProducts), new { name = product.Name }, product);
        }
    }
}
