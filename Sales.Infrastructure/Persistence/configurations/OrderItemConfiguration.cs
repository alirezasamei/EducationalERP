using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Common;
using Sales.Domain.Orders;

namespace Sales.Infrastructure.Persistence.configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem", "Sales");
        builder.HasKey(x => new { x.OrderId, x.ProductId });
        builder.Property(x => x.Quantity).IsRequired();
        builder.ComplexProperty(
            x => x.UnitPrice,
            money =>
            {
                money.Property(x => x.Amount).HasColumnName(nameof(OrderItem.UnitPrice) + "_" + nameof(Money.Amount));
                money.Property(x => x.Currency).HasColumnName(nameof(OrderItem.UnitPrice) + "_" + nameof(Money.Currency));
            }
        );
    }
}
