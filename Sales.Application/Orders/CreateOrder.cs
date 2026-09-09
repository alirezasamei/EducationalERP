using Sales.Domain.Orders;

namespace Sales.Application.Orders;

public class CreateOrder
{
    public void Handle(int customerId, string customerName, int createdUserId, IEnumerable<OrderItem> items)
    {
        Order.Create(customerId, customerName, createdUserId, items);
    }
}
