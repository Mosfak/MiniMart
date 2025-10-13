using Microsoft.EntityFrameworkCore;
using MiniMart.Models;

namespace MiniMart.Data
{
    public class MiniMartDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public MiniMartDbContext(DbContextOptions<MiniMartDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

        }
    }
}
