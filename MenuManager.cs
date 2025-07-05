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
            Console.WriteLine("=== Customer Management System ===");
            Console.WriteLine("1. Manage Customers");
            Console.WriteLine("2. Manage Products");
            Console.WriteLine("3. Manage Orders");
            Console.WriteLine("4. Save Data");
            Console.WriteLine("5. Load Data");
            Console.WriteLine("6. Exit");
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
        DataContext.Customers.Add(newCustomer);

        Console.WriteLine("Customer created successfully");
    }

    void ViewAllCustomer()
    {
        displayHelper.PrintCustomer(DataContext.Customers);
    }

    void FindCustomerByID()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var customer = inputValidator.GetCustomerById(DataContext.Customers, userId);
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
        var customer = inputValidator.GetCustomerById(DataContext.Customers, userId);
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
        var customer = inputValidator.GetCustomerById(DataContext.Customers, userId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }
        DataContext.Customers.Remove(customer);
        Console.WriteLine("Customer successfully deleted");
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

        var customerPartialSearched = DataContext.Customers.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
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
        }

        Console.Write("Enter product stock quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int stockQuantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
        }

        var newProduct = new Product(name, price, stockQuantity);
        DataContext.Products.Add(newProduct);

        Console.WriteLine("Product created successfully");
    }

    void ViewAllProduct()
    {
        displayHelper.PrintProduct(DataContext.Products);
    }

    void FindProductByIdID()
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var product = inputValidator.GetProductById(DataContext.Products, userId);
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
        var product = inputValidator.GetProductById(DataContext.Products, userId);
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
        var product = inputValidator.GetProductById(DataContext.Products, userId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }
        DataContext.Products.Remove(product);

        Console.WriteLine("Customer successfully deleted");
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

        var productPartialSearched = DataContext.Products.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
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
            Console.WriteLine("4. Update Order");
            Console.WriteLine("5. Delete Order");
            Console.WriteLine("6. Exit");
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
                    UpdateOrder();
                    break;
                case "5":
                    DeleteOrder();
                    break;
                case "6":
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
        // getting customer
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;

        }
        var customer = inputValidator.GetCustomerById(DataContext.Customers, customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }

        // getting product
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var product = inputValidator.GetProductById(DataContext.Products, productId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        if (product.StockQuantity == 0)
        {
            Console.WriteLine("Product out of stock");
            return;
        }

        Console.Write("Enter quantity of the order: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }

        orderService.CreateOrder(customer, product, quantity);
    }

    void ViewAllOrders()
    {
        displayHelper.PrintOrder(DataContext.Orders);
    }

    void FindOrderById()
    {
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var order = inputValidator.GetOrderById(DataContext.Orders, orderId);
        if (order == null)
        {
            Console.WriteLine("Order not found");
            return;
        }
        Console.WriteLine($"ID: {order.OrderId} | Customer Name: {order.Customer.Name} | Product Name and Price [{order.Product.Name} - {order.Product.Price}] | Quantity: {order.Quantity} | Date: {order.OrderDate} | Total: {order.TotalAmount}");

    }

    void UpdateOrder()
    {
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var order = inputValidator.GetOrderById(DataContext.Orders, orderId);
        if (order == null)
        {
            Console.WriteLine("Order not found");
            return;
        }

        // getting customer
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;

        }
        var newCustomer = inputValidator.GetCustomerById(DataContext.Customers, customerId);
        if (newCustomer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }

        // getting product
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int newProductId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var newProduct = inputValidator.GetProductById(DataContext.Products, newProductId);
        if (newProduct == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        if (newProduct.StockQuantity == 0)
        {
            Console.WriteLine("Product out of stock");
            return;
        }

        Console.Write("Enter quantity of the order: ");
        if (!int.TryParse(Console.ReadLine(), out int newQuantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }

        order.Customer = newCustomer;
        order.Product = newProduct;
        order.Quantity = newQuantity;
        order.OrderDate = DateTime.Now;
        order.TotalAmount = newQuantity * newProduct.Price;

        Console.WriteLine("Order successfully updated");
    }

    void DeleteOrder()
    {
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var order = inputValidator.GetOrderById(DataContext.Orders, orderId);
        if (order == null)
        {
            Console.WriteLine("Order not found");
            return;
        }
        DataContext.Orders.Remove(order);
        Console.WriteLine("Order successfully deleted");
    }

}
