using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMart.Models;
using MiniMart.Services;

namespace MiniMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWTService _jwtService;

        //In memory Database
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Role = "Admin" },
            //new User { Id = 2, Username = "customer", Role = "Customer" }
        };

        public AuthController(JWTService jwtService)
        {
            _jwtService = jwtService; 
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _users.SingleOrDefault(u => u.Username == request.Username);
            if (user == null)
            {
                var newCustomer = new User
                {
                    Id = _users.Max(u => u.Id) + 1,
                    Username = request.Username,
                    Role = "Customer"
                };
                _users.Add(newCustomer);
                user = newCustomer;
            }

            if (request.Username == "admin" && request.Password == "Admin123")
            {
                var token = _jwtService.GenerateToken(user);
                return Ok(new { token });
            }

            

            var customerToken = _jwtService.GenerateToken(user);
            return Ok(new { token = customerToken });
        }
        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string? Password { get; set; }
        }
    }
}
