using System;

namespace CustomerManagement.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(int id);
    IEnumerable<Order> GetAll();
    IEnumerable<Order> GetByCustomerId(int CustomerId);
    bool Remove(Order order);
    int GetTotalOrderCount();
    decimal GetAverageOrderValue();
}
