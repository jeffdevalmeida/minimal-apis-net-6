using CustomerMinimals.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerMinimals.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=customers.db;Cache=Shared");
    }
}
