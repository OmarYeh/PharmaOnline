using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Final_Project.Models;

namespace Final_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<BasketItems> BasketItems { get; set; }

    }
}