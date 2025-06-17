using System.Text.RegularExpressions;

namespace CustomerManagement;

public class InputValidator
{
    public bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public Customer GetCustomerById(List<Customer> customers, int id)
    {
        return customers.FirstOrDefault(s => s.CustomerId == id)!;
    }

    public Product GetProductById(List<Product> products, int id)
    {
        return products.FirstOrDefault(p => p.ProductId == id)!;
    }

    public Order GetOrderById(List<Order> orders, int id)
    {
        return orders.FirstOrDefault(p => p.OrderId == id)!;
    }
}
