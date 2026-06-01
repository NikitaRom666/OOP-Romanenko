using System.Collections.Generic;
using System.Linq;

namespace Lab39.Services;

public class OrderRepository : IOrderRepository
{
    private readonly List<Order> _store = new();

    public Task<Order?> GetByIdAsync(int id) =>
        Task.FromResult(_store.FirstOrDefault(o => o.Id == id));

    public Task<IEnumerable<Order>> GetAllAsync() =>
        Task.FromResult<IEnumerable<Order>>(_store);

    public Task AddAsync(Order order)
    {
        _store.Add(order);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        _store.RemoveAll(o => o.Id == id);
        return Task.CompletedTask;
    }
}
