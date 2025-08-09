using System.Text.RegularExpressions;

namespace CustomerManagement;

public class InputValidator
{
    public bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public void ConfirmCustomerDeletion(DataContext dataContext, Customer customer)
    {
        Console.Write("Are you sure you want to delete it? (y/n): ");
        string? userConfirmation = Console.ReadLine()?.Trim().ToLower();

        if (userConfirmation == "n" || userConfirmation == "no")
        {
            return;
        }
        else if (userConfirmation == "y" || userConfirmation == "yes")
        {
            var customerOrders = dataContext.Orders.Where(o => o.CustomerId == customer.CustomerId).ToList();
            if (customerOrders.Count > 0)
            {
                Console.WriteLine("Deletion not allowed: the customer has at least one associated order.");
                return;
            }
            dataContext.Customers.Remove(customer);
            Console.WriteLine("Customer successfully deleted");
        }
        else
        {
            Console.WriteLine("Invalid input!");
            return;
        }
    }

    public void ConfirmProductDeletion(DataContext dataContext, Product product)
    {
        Console.Write("Are you sure you want to delete it? (y/n): ");
        string? userConfirmation = Console.ReadLine()?.Trim().ToLower();
                                        
        if (userConfirmation == "n" || userConfirmation == "no")
        {
            return;
        }
        else if (userConfirmation == "y" || userConfirmation == "yes")
        {
            var productOrders = dataContext.Orders.Where(o => o.ProductId == product.ProductId).ToList();
            if (productOrders.Count > 0)
            {
                Console.WriteLine("Deletion not allowed: the product has at least one associated order.");
                return;
            }
            dataContext.Products.Remove(product);
            Console.WriteLine("Product successfully deleted");
        }
        else 
        {
            Console.WriteLine("Invalid input!");
            return;
        }
    }

    public void ConfirmOrderDeletion(DataContext dataContext, Order order)
    {
        Console.Write("Are you sure you want to delete it? (y/n): ");
        string? userConfirmation = Console.ReadLine()?.Trim().ToLower();

        if (userConfirmation == "n" || userConfirmation == "no")
        {
            return;
        }
        else if (userConfirmation == "y" || userConfirmation == "yes")
        {
            dataContext.Orders.Remove(order);
            Console.WriteLine("Order successfully deleted");
        }
        else
        {
            Console.WriteLine("Invalid input!");
            return;
        }
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
