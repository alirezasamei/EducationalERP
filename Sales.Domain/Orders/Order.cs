using Sales.Domain.Common;

namespace Sales.Domain.Orders;

public class Order
{
    private Order(int Id, int CustomerId, string CustomerName, Money TotalAmount, DateTime OrderDate, int CreatedUserId, bool Confirmed, IReadOnlyCollection<OrderItem> Items)
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


    public static Order CreateNew(int customerId, string customerName, int createdUserId, IReadOnlyCollection<OrderItem> items)
    {
        if (items.Count == 0)
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
    public void AddItem(OrderItem newItem)
    {
        CheckConfirm();
        _items.Add(newItem);
    }
    public void AddItems(List<OrderItem> newItems)
    {
        CheckConfirm();
        _items.AddRange(newItems);
    }
    public void RemoveItem(int productId)
    {
        CheckConfirm();
        var item = _items.FirstOrDefault(item => item.ProductId == productId)
            ?? throw new KeyNotFoundException($"Product with id : {{{productId}}} not found in this order");
        _items.Remove(item);
    }
    public void RemoveItems(List<int> productIds)
    {
        CheckConfirm();
        foreach (var productId in productIds)
            RemoveItem(productId);
    }
    public void UpdateOrderItemQuantity(int productId, int quantity)
    {
        CheckConfirm();
        var index = _items.FindIndex(x => x.ProductId == productId);

        if (index == -1)
            throw new InvalidOperationException("Order item not found.");

        _items[index] = _items[index] with
        {
            Quantity = quantity
        };
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
