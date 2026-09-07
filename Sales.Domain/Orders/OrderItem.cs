using Sales.Domain.Common;

namespace Sales.Domain.Orders;

public record class OrderItem
{
    public int OrderId { get; init; }
    public int ProductId { get; init; }
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem(int orderId, int productId, Money unitPrice, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static OrderItem Create(int orderId, int productId, Money unitPrice, int quantity)
    {
        return new OrderItem(orderId, productId, unitPrice, quantity);
    }

    public void ChangeQuantity(int quantity, out Money increasedAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        increasedAmount = UnitPrice * (quantity - Quantity);
        Quantity = quantity;
    }
}
