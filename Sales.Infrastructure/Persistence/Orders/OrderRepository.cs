using Sales.Application.Orders.Persistence;
using Sales.Domain.Orders;
using Sales.Infrastructure.Context;

namespace Sales.Infrastructure.Persistence.Orders;

public class OrderRepository(SalesDBContext context) : IOrderRepository
{
    public async Task Add(Order order)
    {
        await context.Orders.AddAsync(order);
    }
}
