using Microsoft.EntityFrameworkCore;
using PizzaShop.Entities;

namespace PizzaShop.Contexts
{
    public class PizzaShopDbContext : DbContext
    {
        public PizzaShopDbContext (DbContextOptions<PizzaShopDbContext> options) : base (options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
