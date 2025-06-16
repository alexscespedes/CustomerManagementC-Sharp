using System.Text.RegularExpressions;

namespace CustomerManagement;

public class InputValidator
{
    public bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public Customer CustomerExist(List<Customer> customers, int id)
    {
        var customer = customers.FirstOrDefault(s => s.CustomerId == id);

        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return null!;
        }
        return customer;
    }
}
