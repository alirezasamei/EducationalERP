using Sales.Domain.Orders;

namespace Sales.Application.Orders.Persistence;

public interface IOrderRepository
{
    public Task Add(Order order);
}
