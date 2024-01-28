using Microsoft.EntityFrameworkCore;

namespace LabProject.Data
{
    public class CustomersContext : DbContext
    {
        public CustomersContext(DbContextOptions<CustomersContext> options)
            : base(options)
        {
        }

        public DbSet<LabProject.Models.Customers.Customer> Customer { get; set; } = default!;
    }
}
