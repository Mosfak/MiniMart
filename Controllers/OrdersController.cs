using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMart.Data;
using MiniMart.Models;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(MiniMartDbContext context) : ControllerBase
{
    private readonly MiniMartDbContext _context = context;

    [HttpPost]
    [Authorize(Roles = "Customer")]
    public IActionResult PlaceOrder([FromBody] OrderSummary order)
    {
        if (order == null || order.Items == null || !order.Items.Any())
            return BadRequest("Invalid order data");

        _context.OrderSummary.Add(order);
        _context.SaveChanges();

        return Ok(new { message = "Order placed successfully", orderId = order.OrderSummeryId });
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = "Customer")]
    public IActionResult GetOrders(int userId)
    {
        var orders = _context.OrderSummary
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        return Ok(orders);
    }
}
