namespace CustomerManagement;

public class MenuManager
{
    InputValidator inputValidator = new InputValidator();
    DisplayHelper displayHelper = new DisplayHelper();
    OrderService orderService = new OrderService();
    DataContext dataContext = new DataContext();
    JsonDataRepository jsonDataRepository = new JsonDataRepository();
    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Customer Management System ===");
            Console.WriteLine("1. Manage Customers");
            Console.WriteLine("2. Manage Products");
            Console.WriteLine("3. Manage Orders");
            Console.WriteLine("4. Save Data");
            Console.WriteLine("5. Load Data");
            Console.WriteLine("6. Reports");
            Console.WriteLine("7. Exit");
            Console.Write("Please select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManageCustomers();
                    break;
                case "2":
                    ManageProducts();
                    break;
                case "3":
                    ManageOrders();
                    break;
                case "4":
                    jsonDataRepository.SaveData(dataContext);
                    break;
                case "5":
                    jsonDataRepository.LoadData(dataContext);
                    break;
                case "6":
                    Reports();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    void ManageCustomers()
    {
        while (true)
        {
            Console.WriteLine("=== Customer Management ===");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("3. Find Customer by ID");
            Console.WriteLine("4. Update Customer");
            Console.WriteLine("5. Delete Customer");
            Console.WriteLine("6. Search Customer");
            Console.WriteLine("7. Exit");
            Console.Write("Please select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCustomer();
                    break;
                case "2":
                    ViewAllCustomer();
                    break;
                case "3":
                    FindCustomerByID();
                    break;
                case "4":
                    UpdateCustomer();
                    break;
                case "5":
                    DeleteCustomer();
                    break;
                case "6":
                    SearchCustomerByName();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    void AddCustomer()
    {
        Console.Write("Enter the name: ");
        string name = Console.ReadLine()!;

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Error: name of customer is required");
            return;
        }

        Console.Write("Enter the email: ");
        string email = Console.ReadLine()!;

        if (!inputValidator.IsValidEmail(email))
        {
            Console.WriteLine("Error: email of customer is not valid");
            return;
        }
        var user = dataContext.Customers.FirstOrDefault(c => c.Email == email);
        if (user != null)
        {
            Console.WriteLine("No two customers can have the same email");
            return;
        }

        Console.WriteLine("Select customer type: ");
        Console.WriteLine("1. Regular ");
        Console.WriteLine("2. Premiun");
        Console.Write("Choose an option: ");
        if (!int.TryParse(Console.ReadLine(), out int userType))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
        }

        CustomerType customerType;
        switch (userType)
        {
            case 1:
                customerType = CustomerType.Regular;
                break;
            case 2:
                customerType = CustomerType.Premiun;
                break;
            default:
                Console.WriteLine("Invalid option. Try again");
                return;
        }

        var newCustomer = new Customer(name, email, customerType);
        dataContext.Customers.Add(newCustomer);

        Console.WriteLine("Customer created successfully");
    }

    void ViewAllCustomer()
    {
        displayHelper.PrintCustomer(dataContext.Customers);
    }

    void FindCustomerByID()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var customer = inputValidator.GetCustomerById(dataContext.Customers, userId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }
        Console.WriteLine($"ID: {customer.CustomerId} | Customer: {customer.Name} | Email {customer.Email} | Type: {customer.CustomerType}");
    }

    void UpdateCustomer()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var customer = inputValidator.GetCustomerById(dataContext.Customers, userId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }

        Console.Write("Enter the name to update: ");
        string newName = Console.ReadLine()!;

        if (string.IsNullOrEmpty(newName))
        {
            Console.WriteLine("Error: name of customer is required");
            return;
        }

        Console.Write("Enter the email to update: ");
        string newEmail = Console.ReadLine()!;

        if (!inputValidator.IsValidEmail(newEmail))
        {
            Console.WriteLine("Error: email of customer is not valid");
            return;
        }

        Console.WriteLine("Select customer type to update: ");
        Console.WriteLine("1. Regular ");
        Console.WriteLine("2. Premiun");
        Console.Write("Choose an option: ");
        if (!int.TryParse(Console.ReadLine(), out int newUserType))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
        }

        CustomerType newCustomerType;
        switch (newUserType)
        {
            case 1:
                newCustomerType = CustomerType.Regular;
                break;
            case 2:
                newCustomerType = CustomerType.Regular;
                break;
            default:
                Console.WriteLine("Invalid option. Try again");
                return;
        }

        customer.Name = newName;
        customer.Email = newEmail;
        customer.CustomerType = newCustomerType;

        Console.WriteLine("Customer successfully updated");
    }

    void DeleteCustomer()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var customer = inputValidator.GetCustomerById(dataContext.Customers, userId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }
        Console.WriteLine($"Customer Details: [{customer.CustomerId}] {customer.Name} | {customer.Email} | {customer.CustomerType}");
        inputValidator.ConfirmCustomerDeletion(dataContext, customer);
    }

    void SearchCustomerByName()
    {
        Console.Write("Enter Customer Name: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("the name cannot be empty");
            return;
        }

        var customerPartialSearched = dataContext.Customers.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        displayHelper.PrintCustomer(customerPartialSearched);
    }

    void ManageProducts()
    {
        while (true)
        {
            Console.WriteLine("=== Product Management ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Find Product by ID");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Search Product");
            Console.WriteLine("7. Exit");
            Console.Write("Please select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    ViewAllProduct();
                    break;
                case "3":
                    FindProductByIdID();
                    break;
                case "4":
                    UpdateProduct();
                    break;
                case "5":
                    DeleteProduct();
                    break;
                case "6":
                    SearchProductByName();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    void AddProduct()
    {
        Console.Write("Enter the product name: ");
        string name = Console.ReadLine()!;

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Error: name of product is required");
            return;
        }

        Console.Write("Enter the price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        else if (price <= 0)
        {
            Console.WriteLine("Product prices must be greater than 0");
            return;
        }

        Console.Write("Enter product stock quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int stockQuantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        else if (stockQuantity < 0)
        {
            Console.WriteLine("Stock can't be negative");
            return;
        }



        var newProduct = new Product(name, price, stockQuantity);
        dataContext.Products.Add(newProduct);

        Console.WriteLine("Product created successfully");
    }

    void ViewAllProduct()
    {
        displayHelper.PrintProduct(dataContext.Products);
    }

    void FindProductByIdID()
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var product = inputValidator.GetProductById(dataContext.Products, userId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }
        Console.WriteLine($"ID: {product.ProductId} | Product: {product.Name} | Price {product.Price} | In Stock: {product.StockQuantity}");
    }

    void UpdateProduct()
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var product = inputValidator.GetProductById(dataContext.Products, userId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        Console.Write("Enter the product name to update: ");
        string newName = Console.ReadLine()!;

        if (string.IsNullOrEmpty(newName))
        {
            Console.WriteLine("Error: name of product is required");
            return;
        }

        Console.Write("Enter the price to update: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
        }

        Console.Write("Enter product stock quantity to update: ");
        if (!int.TryParse(Console.ReadLine(), out int newStockQuantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
        }

        product.Name = newName;
        product.Price = newPrice;
        product.StockQuantity = newStockQuantity;

        Console.WriteLine("Customer successfully updated");

    }

    void DeleteProduct()
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var product = inputValidator.GetProductById(dataContext.Products, userId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        Console.WriteLine($"Product Details: [{product.ProductId}] {product.Name} | {product.Price} | {product.StockQuantity}");
        inputValidator.ConfirmProductDeletion(dataContext, product);
    }

    void SearchProductByName()
    {
        Console.Write("Enter Product Name: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("the name cannot be empty");
            return;
        }

        var productPartialSearched = dataContext.Products.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        displayHelper.PrintProduct(productPartialSearched);
    }



    void ManageOrders()
    {
        while (true)
        {
            Console.WriteLine("=== Order Management ===");
            Console.WriteLine("1. Create Order");
            Console.WriteLine("2. View All Orders");
            Console.WriteLine("3. Find Order by ID");
            Console.WriteLine("4. Delete Order");
            Console.WriteLine("5. Exit");
            Console.Write("Please select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateOrder();
                    break;
                case "2":
                    ViewAllOrders();
                    break;
                case "3":
                    FindOrderById();
                    break;
                case "4":
                    DeleteOrder();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    void CreateOrder()
    {
        // customer
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }

        // product
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }

        // quantity
        Console.Write("Enter quantity of the order: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }

        orderService.CreateOrder(customerId, productId, quantity, dataContext);
    }

    void ViewAllOrders()
    {
        displayHelper.PrintOrder(dataContext.Orders);
    }

    void FindOrderById()
    {
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var order = inputValidator.GetOrderById(dataContext.Orders, orderId);
        if (order == null)
        {
            Console.WriteLine("Order not found");
            return;
        }
        Console.WriteLine($"ID: {order.OrderId} | Customer Id: {order.CustomerId} | Product Id[{order.ProductId} | Quantity: {order.Quantity} | Date: {order.OrderDate} | Total: {order.TotalAmount}");

    }

    void DeleteOrder()
    {
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var order = inputValidator.GetOrderById(dataContext.Orders, orderId);
        if (order == null)
        {
            Console.WriteLine("Order not found");
            return;
        }
        Console.WriteLine($"Order Details: [{order.OrderId}] {order.Quantity} | {order.TotalAmount} | {order.OrderDate}");
        inputValidator.ConfirmOrderDeletion(dataContext, order);
    }

    void Reports()
    {
        while (true)
        {
            Console.WriteLine("=== Reports ===");
            Console.WriteLine("1. Total Sales Report");
            Console.WriteLine("2. Customer Order History");
            Console.WriteLine("3. Low Stock Alert");
            Console.WriteLine("4. Exit");
            Console.Write("Please select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    TotalSalesReport();
                    break;
                case "2":
                    CustomerOrderHistory();
                    break;
                case "3":
                    LowStockAlert();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    void TotalSalesReport()
    {
        decimal totalOrderAmount = dataContext.Orders.Sum(o => o.TotalAmount);

        int totalOrderCount = dataContext.Orders.Count;

        var averageOrderValue = dataContext.Orders.Average(o => o.TotalAmount);

        Console.WriteLine("=== Total Sales Report ===");

        Console.WriteLine($"Sum Order Amounts: {totalOrderAmount} | Count of Total Orders: {totalOrderCount} | Average Order Value: {averageOrderValue} |");
    }

    void CustomerOrderHistory()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }

        var customerOrders = dataContext.Orders.Where(o => o.CustomerId == id).ToList();
        if (customerOrders.Count == 0)
        {
            Console.WriteLine($"Customers don't have orders registered.");
        }

        displayHelper.PrintOrder(customerOrders);
    }

    void LowStockAlert()
    {
        var productsLowStock = dataContext.Products.Where(p => p.StockQuantity < 5).ToList();
        displayHelper.PrintProduct(productsLowStock);

        decimal TotalAmount = 0;
        foreach (var product in productsLowStock)
        {
            TotalAmount += product.Price * product.StockQuantity;
        }
        Console.WriteLine($"--- Total Value of Low Stock Products : {TotalAmount}");
    }
}
