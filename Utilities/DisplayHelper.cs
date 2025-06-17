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

    public void PrintProduct(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Product not found");
            return;
        }

        foreach (var p in products)
        {
            Console.WriteLine($"ID: {p.ProductId} | Product: {p.Name} | Price {p.Price} | In Stock: {p.StockQuantity}");
        }
    }
}
