namespace Lab39.Services;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
}

public interface IOrderService
{
    Order? GetById(int id);
}

public class OrderService : IOrderService
{
    private readonly List<Order> _orders = new();

    public Order? GetById(int id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }
}
