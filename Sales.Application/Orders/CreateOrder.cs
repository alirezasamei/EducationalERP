using Sales.Application.Orders.Persistence;
using Sales.Domain.Orders;

namespace Sales.Application.Orders;

public class CreateOrder(IOrderRepository orderRepository)
{
    public async Task Handle(int customerId, string customerName, int createdUserId, IEnumerable<OrderItem> items)
    {
        var model = Order.Create(customerId, customerName, createdUserId, items);
        await orderRepository.Add(model);
    }
}