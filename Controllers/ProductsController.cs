using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MiniMart.Models;
using MiniMart.Services;

namespace MiniMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Data.MiniMartDbContext _context;

        public ProductsController(Data.MiniMartDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Customer")]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct([FromBody] Models.Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.Name) || product.Price < 0)
            {
                return BadRequest("Invalid product data.");
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProducts), new { name = product.Name }, product);
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct(int productId)
        {
            var product = _context.Set<Product>().Find(productId);

            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {productId} not found." });
            }

            _context.Set<Product>().Remove(product);
            _context.SaveChanges();

            return Ok(new { message = $"Product '{product.Name}' has been deleted successfully.", productId });
        }

    }
}
