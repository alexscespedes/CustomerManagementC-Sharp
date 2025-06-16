namespace CustomerManagement;

public class DisplayHelper
{
    public void PrintCustomer(List<Customer> customers)
    {
        if (customers.Count == 0)
        {
            Console.WriteLine("Customer not found");
            return;
        }

        foreach (var c in customers)
        {
            Console.WriteLine($"ID: {c.CustomerId} | Customer: {c.Name} | Email {c.Email} | Type: {c.CustomerType}");
        }
    }
}
