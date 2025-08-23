using System;
using System.Text.RegularExpressions;

namespace CustomerManagement.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _dataContext;

    public CustomerRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public void Add(Customer customer)
    {
        _dataContext.Customers.Add(customer);
    }

    public bool EmailExists(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public IEnumerable<Customer> GetAll()
    {
        return _dataContext.Customers.ToList();
    }

    public Customer? GetById(int id)
    {
        return _dataContext.Customers.FirstOrDefault(c => c.CustomerId == id);
    }

    public void Remove(Customer customer)
    {
        _dataContext.Customers.Remove(customer);
    }

    public IEnumerable<Customer> SearchByName(string name)
    {
        return _dataContext.Customers
            .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void Update(Customer customer)
    {
        var existingCustomer = GetById(customer.CustomerId);
        if (existingCustomer != null)
        {
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.CustomerType = customer.CustomerType;
        }
    }
}
