using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data
{
    public class Appdbcontext:DbContext
    {
        public Appdbcontext(DbContextOptions<Appdbcontext> options) : base(options)
        {
        }

        public DbSet<Retailer>Retailer { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<SuperProduct> SuperProduct { get; set; }
        public DbSet<Purchase> Purchase { get; set; }


    }
}
