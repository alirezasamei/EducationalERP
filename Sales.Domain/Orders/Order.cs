using Sales.Domain.Common;

namespace Sales.Domain.Orders;

public class Order
{
    private Order(int id, int customerId, string customerName, Money totalAmount, DateTime orderDate, int createdUserId, bool confirmed, IEnumerable<OrderItem> items)
    {
        Id = id;
        CustomerId = customerId;
        CustomerName = customerName;
        TotalAmount = totalAmount;
        OrderDate = orderDate;
        CreatedUserId = createdUserId;
        Confirmed = confirmed;
        _items = [.. items];
        CheckDuplicateProduct();
    }


    public static Order Create(int customerId, string customerName, int createdUserId, IEnumerable<OrderItem> items)
    {
        List<OrderItem> itemList = [.. items];
        if (itemList.Count == 0)
            throw new Exception("Order has no items"); // must be changed
        var id = GenerateNewId();
        var totalAmount = itemList.Select(item => item.UnitPrice * item.Quantity).SumInSameCurrencies();
        var order = new Order(id, customerId, customerName, totalAmount, DateTime.Now, createdUserId, false, itemList);
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
        CheckDuplicateProduct();
    }
    public void AddItems(IEnumerable<OrderItem> items)
    {
        CheckConfirm();
        TotalAmount += items.Select(item => item.Quantity * item.UnitPrice).SumInSameCurrencies();
        _items.AddRange(items);
        CheckDuplicateProduct();
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

    private void CheckDuplicateProduct()
    {
        var productIds = _items.Select(x => x.ProductId).OrderBy(x => x);
        int lastProductId = 0;
        foreach (var productId in productIds)
        {
            if (productId == lastProductId)
                throw new Exception($"Product with id : {{{productId}}} is duplicated");
            lastProductId = productId;
        }
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

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items;
}
