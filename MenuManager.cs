namespace CustomerManagement;

public class MenuManager
{
        List<Customer> customers = new List<Customer>();
        List<Product> products = new List<Product>();
        List<Order> orders = new List<Order>();
        InputValidator inputValidator = new InputValidator();
        DisplayHelper displayHelper = new DisplayHelper();
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("=== Customer Management System ===");
            Console.WriteLine("1. Manage Customers");
            Console.WriteLine("2. Manage Products");
            Console.WriteLine("3. Manage Orders");
            Console.WriteLine("4. Exit");
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
            Console.WriteLine("6. Exit");
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
                customerType = CustomerType.Regular;
                break;
            default:
                Console.WriteLine("Invalid option. Try again");
                return;
        }

        var newCustomer = new Customer(name, email, customerType);
        customers.Add(newCustomer);
    }

    void ViewAllCustomer()
    {
        displayHelper.PrintCustomer(customers);
    }

    void FindCustomerByID()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
        }
        var customer = inputValidator.CustomerExist(customers, userId);
        Console.WriteLine($"ID: {customer.CustomerId} | Customer: {customer.Name} | Email {customer.Email} | Type: {customer.CustomerType}");
    }

    void UpdateCustomer()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
        }
        var customer = inputValidator.CustomerExist(customers, userId);

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
    }

    void DeleteCustomer()
    {
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input! Please enter a valid integer");
        }
        var customer = inputValidator.CustomerExist(customers, userId);
        customers.Remove(customer);
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
            Console.WriteLine("6. Exit");
            Console.Write("Please select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Add
                    break;
                case "2":
                    // View All
                    break;
                case "3":
                // Find by ID
                case "4":
                    // Update
                    break;
                case "5":
                    // Delete
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

    void ManageOrders()
    {
        while (true)
        {
            Console.WriteLine("=== Order Management ===");
            Console.WriteLine("1. Add Order");
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
                    // Add
                    break;
                case "2":
                    // View All
                    break;
                case "3":
                // Find by ID
                case "4":
                    // Update
                    break;
                case "5":
                    // Delete
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
    

}
