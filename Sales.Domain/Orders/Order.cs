using Sales.Domain.Common;

namespace Sales.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Money TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Invoiced { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CreatedUserId { get; set; }
    }
}
