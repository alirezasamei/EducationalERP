using Sales.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructure.Persistence.Orders;

public interface IOrderRepository
{
    public Task Add(Order order);
    public Task Save();
}
