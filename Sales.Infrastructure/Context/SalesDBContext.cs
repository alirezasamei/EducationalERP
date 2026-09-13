using Microsoft.EntityFrameworkCore;
using Sales.Domain.Orders;
using Sales.Infrastructure.Persistence.configurations;

namespace Sales.Infrastructure.Context;

public class SalesDBContext(DbContextOptions<SalesDBContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
