using Sales.Domain.Common;

namespace Sales.Domain.Orders;

public record class  OrderItem(int OrderId, int ProductId, Money UnitPrice, int Quantity)
{
}
