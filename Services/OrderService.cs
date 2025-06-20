namespace CustomerManagement;

public class OrderService
{    
    public void CreateOrder(Customer customer, Product product, int quantity)
    {
        // Business logic from here.
        // 1. Stock availability check
        if (product.StockQuantity < quantity)
        {
            Console.Error.WriteLine($"Error: Not enough {product.Name} in stock");
            return;
        }
        var newOrder = new Order(customer, product, quantity);


        // 2. Automatic stock reduction
        var productStockUpdated = product.StockQuantity -= quantity;
        var updateProduct = DataContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
        if (updateProduct == null)
        {
            Console.Error.Write("Product not found in the list");
            return;
        }
        updateProduct.StockQuantity = productStockUpdated;

        // 3. Customer type discount
        if (customer.CustomerType == CustomerType.Premiun)
        {
            decimal subTotal = quantity * product.Price;
            decimal discount = subTotal * 0.10m;
            newOrder.TotalAmount = subTotal - discount;
        }
        else
        {
            newOrder.TotalAmount = quantity * product.Price;
        }

        // 4. Order total calculation
        DataContext.Orders.Add(newOrder);
        Console.WriteLine("Order created successfully");
    }
}
