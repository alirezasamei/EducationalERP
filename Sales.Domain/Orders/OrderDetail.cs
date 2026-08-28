using Sales.Domain.Common;

namespace Sales.Domain.Orders
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Money UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
