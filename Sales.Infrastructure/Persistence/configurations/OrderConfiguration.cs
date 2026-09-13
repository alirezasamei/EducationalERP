using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Common;
using Sales.Domain.Orders;

namespace Sales.Infrastructure.Persistence.configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order", "Sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerName).HasMaxLength(100);
        builder.ComplexProperty(
            x => x.TotalAmount,
            money =>
            {
                money.Property(x => x.Amount).HasColumnName(nameof(Order.TotalAmount) + "_" + nameof(Money.Amount));
                money.Property(x => x.Currency).HasColumnName(nameof(Order.TotalAmount) + "_" + nameof(Money.Currency));
            }
        );
        builder.OwnsMany(
            x => x.Items,
            item => item.WithOwner().HasForeignKey(x => x.OrderId)
        );
    }
}
