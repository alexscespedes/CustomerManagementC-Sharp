namespace CustomerManagement;

public class MenuManager
{
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly DisplayHelper _displayHelper;
    private readonly JsonDataRepository _jsonDataRepository;
    private readonly DataContext _dataContext;

    public MenuManager(
        ICustomerService customerService,
        IProductService productService,
        IOrderService orderService,
        DisplayHelper displayHelper,
        JsonDataRepository jsonDataRepository,
        DataContext dataContext)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _displayHelper = displayHelper ?? throw new ArgumentNullException(nameof(displayHelper));
        _jsonDataRepository = jsonDataRepository ?? throw new ArgumentNullException(nameof(jsonDataRepository));
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }
    
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
                    SaveData();
                    break;
                case "5":
                    LoadData();
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

    private void SaveData()
    {
        try
        {
            _jsonDataRepository.SaveData(_dataContext);
            Console.WriteLine("Data saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
        Console.ReadKey();
    }

    private void LoadData()
    {
        try
        {
            _jsonDataRepository.LoadData(_dataContext);
            Console.WriteLine("Data loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
        Console.ReadKey();
    }

    void ManageCustomers()
    {
        while (true)
        {
            Console.Clear();
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

    private void AddCustomer()
    {
        Console.Write("Enter the name: ");
        string name = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter the email: ");
        string email = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Select customer type: ");
        Console.WriteLine("1. Regular ");
        Console.WriteLine("2. Premiun");
        Console.Write("Choose an option: ");

        if (!int.TryParse(Console.ReadLine(), out int userType) || (userType != 1 && userType != 2))
        {
            Console.WriteLine("Invalid input! Please select 1 or 2.");
            Console.ReadKey();
            return;
        }

        CustomerType customerType = userType == 1 ? CustomerType.Regular : CustomerType.Premium;

        bool success = _customerService.CreateCustomer(name, email, customerType);

        if (success)
        {
            Console.WriteLine("Customer created successfully!");
        }
        else
        {
            Console.WriteLine("Failed to create customer. Please check your input.");
        }

        Console.ReadKey();
    }

    private void ViewAllCustomer()
    {
        var customers = _customerService.GetAllCustomers();
        _displayHelper.PrintCustomer(customers);
        Console.ReadKey();
    }

    private void FindCustomerByID()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }
        var customer = _customerService.GetCustomerById(customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found");
        }
        else
        {
            Console.WriteLine($"ID: {customer.CustomerId} | Customer: {customer.Name} | Email {customer.Email} | Type: {customer.CustomerType}");
        }
        Console.ReadKey();
    }

    private void UpdateCustomer()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }

        var existingCustomer = _customerService.GetCustomerById(customerId);
        if (existingCustomer == null)
        {
            Console.WriteLine("Customer not found");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Current customer: {existingCustomer.Name} |{existingCustomer.Email} | {existingCustomer.CustomerType}");

        Console.Write("Enter new name: ");
        string newName = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter new email ");
        string newEmail = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Select customer type to update: ");
        Console.WriteLine("1. Regular ");
        Console.WriteLine("2. Premiun");
        Console.Write("Choose an option: ");

        if (!int.TryParse(Console.ReadLine(), out int userType) || (userType != 1 && userType != 2))
        {
            Console.WriteLine("Invalid input! Please select 1 or 2.");
            Console.ReadKey();
            return;
        }

        CustomerType newCustomerType = userType == 1 ? CustomerType.Regular : CustomerType.Premium;

        bool success = _customerService.UpdateCustomer(customerId, newName, newEmail, newCustomerType);

        if (success)
        {
            Console.WriteLine("Customer updated successfully!");
        }
        else
        {
            Console.WriteLine("Failed to update customer. Please check your input.");
        }

        Console.ReadKey();
    }

    private void DeleteCustomer()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }
        var customer = _customerService.GetCustomerById(customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            Console.ReadKey();
            return;
        }
        Console.WriteLine($"Customer Details: [{customer.CustomerId}] {customer.Name} | {customer.Email} | {customer.CustomerType}");
        Console.Write("Are you sure you want to delete this customer? (y/n): ");

        string? confirmation = Console.ReadLine();
        if (confirmation?.ToLower() == "y" || confirmation?.ToLower() == "yes")
        {
            bool success = _customerService.DeleteCustomer(customerId);
            if (success)
            {
                Console.WriteLine("Customer deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete customer.");
            }
        }
        else
        {
            Console.WriteLine("Delete operation cancelled.");
        }
        Console.ReadKey();
    }

    private void SearchCustomerByName()
    {
        Console.Write("Enter Customer Name: ");
        string? name = Console.ReadLine();

        var customers = _customerService.SearchCustomersByName(name ?? string.Empty);
        _displayHelper.PrintCustomer(customers);
        Console.ReadKey();
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

    private void AddProduct()
    {
        Console.Write("Enter the product name: ");
        string name = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter the price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter product stock quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int stockQuantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }

        bool success = _productService.CreateProduct(name, price, stockQuantity);

        if (success)
        {
            Console.WriteLine("Product created successfully");
        }

        else
        {
            Console.WriteLine("Failed to create product. Please check your input.");
        }

        Console.ReadKey();
    }

    private void ViewAllProduct()
    {
        var products = _productService.GetAllProducts();
        _displayHelper.PrintProduct(products);
        Console.ReadKey();
    }

    private void FindProductByIdID()
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }
        var product = _productService.GetProductById(productId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
        }
        else
        {
            Console.WriteLine($"ID: {product.ProductId} | Product: {product.Name} | Price {product.Price} | Stock: {product.StockQuantity}");
        }
        Console.ReadKey();
    }

    private void UpdateProduct()
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var existingProduct = _productService.GetProductById(productId);
        if (existingProduct == null)
        {
            Console.WriteLine("Product not found");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Current product: {existingProduct.Name} | Price {existingProduct.Price:C} | Stock: {existingProduct.StockQuantity}");


        Console.Write("Enter new product name: ");
        string newName = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter new price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter new stock quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int newStockQuantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }

        bool success = _productService.UpdateProduct(productId, newName, newPrice, newStockQuantity);

        if (success)
        {
            Console.WriteLine("Product updated successfully");
        }
        else
        {
            Console.WriteLine("Failed to update product. Please check your input.");
        }

        Console.ReadKey();
    }

    private void DeleteProduct()
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var product = _productService.GetProductById(productId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Product Details: [{product.ProductId}] {product.Name} | {product.Price} | {product.StockQuantity}");
        Console.Write("Are you sure you want to delete this product? (y/n): ");

        string? confirmation = Console.ReadLine();
        if (confirmation?.ToLower() == "y" || confirmation?.ToLower() == "yes")
        {
            bool success = _productService.DeleteProduct(productId);
            if (success)
            {
                Console.WriteLine("product deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete product.");
            }
        }
        else
        {
            Console.WriteLine("Delete operation cancelled.");
        }
        Console.ReadKey();
    }

    private void SearchProductByName()
    {
        Console.Write("Enter Product Name: ");
        string? name = Console.ReadLine();

        var products = _productService.SearchProductsByName(name!);
        _displayHelper.PrintProduct(products);
        Console.ReadKey();
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

    private void CreateOrder()
    {
        // customer
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }

        // product
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }

        // quantity
        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }

        bool success = _orderService.CreateOrder(customerId, productId, quantity);

        if (success)
        {
            Console.WriteLine("Order created successfully!");
        }
        else
        {
            Console.WriteLine("Failed to create order. Please verify customer, product, and stock availability.");
        }

        Console.ReadKey();
    }

    private void ViewAllOrders()
    {
        var orders = _orderService.GetAllOrders();
        _displayHelper.PrintOrder(orders);
        Console.ReadKey();
    }

    private void FindOrderById()
    {
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }
        var order = _orderService.GetOrderById(orderId);
        if (order == null)
        {
            Console.WriteLine("Order not found");
        }
        else
        {
            Console.WriteLine($"ID: {order.OrderId} | Customer Id: {order.CustomerId} | Product Id: [{order.ProductId} | Quantity: {order.Quantity} | Date: {order.OrderDate} | Total: {order.TotalAmount}");
        }
        Console.ReadKey();
    }

    private void DeleteOrder()
    {
        Console.Write("Enter Order ID: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            return;
        }
        var order = _orderService.GetOrderById(orderId);
        if (order == null)
        {
            Console.WriteLine("Order not found");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Order Details: ID: [{order.OrderId}] | Quantity: {order.Quantity} | Total: {order.TotalAmount:C} | Date: {order.OrderDate:yyyy-MM-dd}");
        Console.Write("Are you sure you want to delete this order? (y/n): ");

        string? confirmation = Console.ReadLine();
        if (confirmation?.ToLower() == "y" || confirmation?.ToLower() == "yes")
        {
            bool success = _orderService.DeleteOrder(orderId);
            if (success)
            {
                Console.WriteLine("order deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete order.");
            }
        }
        else
        {
            Console.WriteLine("Delete operation cancelled.");
        }
        Console.ReadKey();
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

    private void TotalSalesReport()
    {
        var (TotalAmount, orderCount, averageValue) = _orderService.GetSalesReport();

        Console.WriteLine("=== Total Sales Report ===");
        Console.WriteLine($"Total Sales Amount: {TotalAmount:C}");
        Console.WriteLine($"Total Number of Orders: {orderCount}");
        Console.WriteLine($"Total Sales Amount: {averageValue:C}");

        Console.ReadKey();
    }

    private void CustomerOrderHistory()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
            Console.ReadKey();
            return;
        }

        var customerOrders = _orderService.GetCustomerOrderHistory(customerId);
        if (customerOrders.Any())
        {
            Console.WriteLine($"No orders found for this customer.");
        }
        else
        {
            Console.WriteLine($"=== Order History for Customer ID: {customerId} ===");
            _displayHelper.PrintOrder(customerOrders);
        }

        Console.ReadKey();
    }

    private void LowStockAlert()
    {
        var lowStockProducts = _productService.GetLowStockProducts();
        var totalValue = _productService.CalculateLowStockValue();

        Console.WriteLine($"== Low Stock Alert (Less than 5 items) ===");

        if (!lowStockProducts.Any())
        {
            Console.WriteLine("No products with low stock.");
        }
        else
        {
            _displayHelper.PrintProduct(lowStockProducts);
            Console.WriteLine($"--- Total Value of Low Stock Products: {totalValue:C} ---");
        }

        Console.ReadKey();
    }
}
