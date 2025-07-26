namespace CustomerManagement;

public class OrderService
{    
    InputValidator inputValidator = new InputValidator();

    public void CreateOrder(int customerId, int productId, int quantity, DataContext dataContext)
    {
        //Getting objects
        var customer = inputValidator.GetCustomerById(dataContext.Customers, customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }

        var product = inputValidator.GetProductById(dataContext.Products, productId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        // Business logic from here.
        // 1. Stock availability check
        if (product.StockQuantity == 0 || product.StockQuantity < quantity)
        {
            Console.WriteLine("Product out of stock or Not enough {product.Name} in stock");
            return;
        }

        var newOrder = new Order(customerId, productId, quantity);

        // 2. Automatic stock reduction
        var productStockUpdated = product.StockQuantity -= quantity;
        var updateProduct = dataContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
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
        dataContext.Orders.Add(newOrder);
        Console.WriteLine("Order created successfully");
    }
}
