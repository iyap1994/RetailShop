using System.Data.Entity;
using RetailShop.Model;

namespace RetailShop.Common
{
    internal class RetailShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().MapToStoredProcedures();
            modelBuilder.Entity<Order>().MapToStoredProcedures();
            base.OnModelCreating(modelBuilder);
        }

    }
}
