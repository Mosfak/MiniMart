// Services/UserService.cs
using Microsoft.AspNetCore.Authorization;
using MiniMart.Data;
using MiniMart.Models;

namespace MiniMart.Services
{
    [Authorize(Roles = "Admin,Customer")]

    public class UserService
    {
        public enum Role
        {
            Admin,
            Customer
        }
        private readonly MiniMartDbContext _context;

        public UserService(MiniMartDbContext context)
        {
            _context = context;
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User CreateCustomer(string username)
        {
            var user = new User
            {
                Username = username,
                Role = Role.Customer.ToString()
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User CreateAdmin(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Role = Role.Admin.ToString()
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User DeleteUser(string username)
        {
            var user = GetUserByUsername(username);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }

    }
}
