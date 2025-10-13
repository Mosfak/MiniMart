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
            if(request.Password != null)
            {
                var admin = _userService.GetAdmin(request.Username, request.Password);
                if (request.Username == "superuser" && request.Password == "Admin123" && admin == null)
                {
                    admin = _userService.CreateAdmin(request.Username, request.Password);
                    var token = _jwtService.GenerateToken(admin); 
                    return Ok(new { token });
                }
                if(admin == null)
                {
                    return NotFound("Wrong credentials");
                }
                var adminToken = _jwtService.GenerateToken(admin);
                return Ok(new { token = adminToken });
            }

            var user = _userService.GetUserByUsername(request.Username);
            if (user == null)
                 user = _userService.CreateCustomer(request.Username);
            
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
