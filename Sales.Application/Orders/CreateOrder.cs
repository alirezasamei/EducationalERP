using Sales.Domain.Orders;
using Sales.Infrastructure.Persistence.Orders;

namespace Sales.Application.Orders;

public class CreateOrder(IOrderRepository OrderRepository)
{
    public async Task Handle(int customerId, string customerName, int createdUserId, IEnumerable<OrderItem> items)
    {
        var model = Order.Create(customerId, customerName, createdUserId, items);
        await OrderRepository.Add(model);
        await OrderRepository.Save();
    }
}