using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMart.Models;
using MiniMart.Services;


namespace MiniMart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Data.MiniMartDbContext _context;
        private readonly UserService _userService;

        public UsersController(Data.MiniMartDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Set<User>().ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Set<User>().Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] User newUser)
        {
            User user; 
            if (newUser == null)
            {
                return BadRequest("User data is required.");
            }
            try
            {
                user = _userService.CreateCustomer(newUser.Username);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Users_Username") == true)
                {
                    return BadRequest("Username already exists.");
                }
                throw;
            }
            return Ok(user);
        }




    }
}
