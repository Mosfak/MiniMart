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
        private readonly UserService _userService;

        

        public AuthController(JWTService jwtService, UserService userService)
        {
            _jwtService = jwtService;
            _userService=userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetUserByUsername(request.Username);
            if (user == null)
                 _userService.CreateCustomer(request.Username);

            user = _userService.GetUserByUsername(request.Username);
            if (request.Username == "admin" && request.Password == "Admin123" || user.Role == UserService.Role.Admin.ToString())
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
