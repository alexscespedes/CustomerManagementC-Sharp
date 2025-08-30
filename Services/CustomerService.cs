
using CustomerManagement.Repositories;

namespace CustomerManagement;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly InputValidator _inputValidator;

    public CustomerService(ICustomerRepository customerRepository, InputValidator inputValidator)
    {
        _customerRepository = customerRepository;
        _inputValidator = inputValidator;
    }

    public bool CreateCustomer(string name, string email, CustomerType customerType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (!_inputValidator.IsValidEmail(email))
        {
            return false;
        }

        if (_customerRepository.EmailExists(email))
        {
            return false;
        }

        var customer = new Customer(name, email, customerType);
        _customerRepository.Add(customer);
        return true;
    }

    public bool DeleteCustomer(int id)
    {
        var customer = _customerRepository.GetById(id);
        if (customer == null)
        {
            return false;
        }

        _customerRepository.Remove(customer);
        return true;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _customerRepository.GetAll();
    }

    public Customer? GetCustomerById(int id)
    {
        return _customerRepository.GetById(id);
    }

    public bool IsEmailUnique(string email)
    {
        return !_customerRepository.EmailExists(email);
    }

    public IEnumerable<Customer> SearchCustomerByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Enumerable.Empty<Customer>();
        }

        return _customerRepository.SearchByName(name);
    }

    public bool UpdateCustomer(int id, string name, string email, CustomerType customerType)
    {
        var existingCustomer = _customerRepository.GetById(id);
        if (existingCustomer == null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (!_inputValidator.IsValidEmail(email))
        {
            return false;
        }

        if (_customerRepository.EmailExists(email) && !existingCustomer.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        existingCustomer.Name = name;
        existingCustomer.Email = email;
        existingCustomer.CustomerType = customerType;
        _customerRepository.Update(existingCustomer);
        return true;
    }
}
