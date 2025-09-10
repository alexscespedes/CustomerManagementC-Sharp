using System;

namespace CustomerManagement.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(int id);
    IEnumerable<Order> GetAll();
    IEnumerable<Order> GetByCustomerId(int CustomerId);
    void Remove(Order order);
    decimal GetTotalSalesAmount();
    int GetTotalOrderCount();
    decimal GetAverageOrderValue();
}
