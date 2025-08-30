namespace CustomerManagement;

public interface ICustomerService
{
    bool CreateCustomer(string name, string email, CustomerType customerType);
    Customer? GetCustomerById(int id);
    IEnumerable<Customer> GetAllCustomers();
    IEnumerable<Customer> SearchCustomerByName(string name);
    bool UpdateCustomer(int id, string name, string email, CustomerType customerType);
    bool DeleteCustomer(int id);
    bool IsEmailUnique(string email);
}
