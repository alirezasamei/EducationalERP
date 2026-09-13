using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Orders;

namespace Sales.Infrastructure.Persistence.configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerName).HasMaxLength(100);
        builder.ComplexProperty(x =>
            x.TotalAmount,
            money =>
            {
                money.Property(x => x.Amount);
                money.Property(x => x.Currency);
            });
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.OrderId);
    }
}
