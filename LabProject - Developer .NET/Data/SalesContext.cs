using Microsoft.EntityFrameworkCore;

namespace LabProject.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        public DbSet<LabProject.Models.Sales.Order> Order { get; set; } = default!;

        public DbSet<LabProject.Models.Sales.OrderDetail> OrderDetail { get; set; } = default!;
    }
}
