using KhumaloWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KhumaloWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Products> ProductsDetails { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
