using Sales.Domain.Common;

namespace Sales.Domain.Orders;

public class Order
{
    private Order(int Id, int CustomerId, string CustomerName, Money TotalAmount, DateTime OrderDate, int CreatedUserId, bool Confirmed, IEnumerable<OrderItem> Items)
    {
        this.Id = Id;
        this.CustomerId = CustomerId;
        this.CustomerName = CustomerName;
        this.TotalAmount = TotalAmount;
        this.OrderDate = OrderDate;
        this.CreatedUserId = CreatedUserId;
        this.Confirmed = Confirmed;
        _items = [.. Items];
    }


    public static Order Create(int customerId, string customerName, int createdUserId, IEnumerable<OrderItem> items)
    {
        if (!items.Any())
            throw new Exception("Order has no items"); // must be changed
        var id = GenerateNewId();
        var totalAmount = items.Select(item => item.UnitPrice * item.Quantity).SumInSameCurrencies();
        var order = new Order(id, customerId, customerName, totalAmount, DateTime.Now, createdUserId, false, items);
        return order;
    }
    public void Confirm()
    {
        CheckConfirm();
        if (_items.Count == 0)
            throw new Exception("Order has no items"); // must be changed
        Confirmed = true;
    }
    public void AddItem(OrderItem item)
    {
        CheckConfirm();
        TotalAmount += item.Quantity * item.UnitPrice;
        _items.Add(item);
    }
    public void AddItems(IEnumerable<OrderItem> newItems)
    {
        CheckConfirm();
        TotalAmount += newItems.Select(item => item.Quantity * item.UnitPrice).SumInSameCurrencies();
        _items.AddRange(newItems);
    }
    public void RemoveItem(int productId)
    {
        CheckConfirm();
        var item = GetOrderItemByProductId(productId);

        TotalAmount -= item.Quantity * item.UnitPrice;
        _items.Remove(item);
    }
    public void RemoveItems(IEnumerable<int> productIds)
    {
        CheckConfirm();
        foreach (var productId in productIds)
            RemoveItem(productId);
    }
    public void UpdateOrderItemQuantity(int productId, int quantity)
    {
        CheckConfirm();
        var item = GetOrderItemByProductId(productId);

        item.ChangeQuantity(quantity, out var amountDelta);

        TotalAmount += amountDelta;
    }

    public OrderItem GetOrderItemByProductId(int productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId)
            ?? throw new KeyNotFoundException($"Product with id : {{{productId}}} not found in this order");
        return item;
    }

    private void CheckConfirm()
    {
        if (Confirmed)
            throw new Exception("Order is confirmed and is not editable");
    }

    private static int GenerateNewId() // must be changed
    {
        int newId = 1;
        return newId;
    }

    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public Money TotalAmount { get; private set; }
    public DateTime OrderDate { get; private set; }
    public int CreatedUserId { get; private set; }
    public bool Confirmed { get; private set; }

    private readonly List<OrderItem> _items;
    public IReadOnlyCollection<OrderItem> Items => _items;
}
