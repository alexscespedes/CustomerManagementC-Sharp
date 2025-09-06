using System;

namespace CustomerManagement.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _dataContext;

    public OrderRepository(DataContext dataContext)
    {
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }
    public void Add(Order order)
    {
        _dataContext.Orders.Add(order);
    }

    public IEnumerable<Order> GetAll()
    {
        return _dataContext.Orders.ToList();
    }

    public decimal GetAverageOrderValue()
    {
        return _dataContext.Orders.Sum(o => o.TotalAmount);
    }

    public IEnumerable<Order> GetByCustomerId(int customerId)
    {
        return _dataContext.Orders.
            Where(o => o.CustomerId == customerId).ToList();
    }

    public Order? GetById(int id)
    {
        return _dataContext.Orders.FirstOrDefault(o => o.OrderId == id);
    }

    public int GetTotalOrderCount()
    {
        return _dataContext.Orders.Count();
    }

    public bool Remove(Order order)
    {
        return _dataContext.Orders.Remove(order);
    }
}
