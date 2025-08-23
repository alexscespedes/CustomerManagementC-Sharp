using System;

namespace CustomerManagement.Repositories;

public interface ICustomerRepository
{
    void Add(Customer customer);
    Customer? GetById(int id);
    IEnumerable<Customer> GetAll();
    IEnumerable<Customer> SearchByName(string name);
    bool EmailExists(string email);
    void Update(Customer customer);
    void Remove(Customer customer);
}
