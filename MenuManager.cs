namespace CustomerManagement;

public class MenuManager
{
    public void MainMenu()
    {
        List<Customer> customers = new List<Customer>();
        List<Product> products = new List<Product>();
        List<Order> orders = new List<Order>();

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

    static void ManageCustomers()
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

    static void ManageProducts()
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
    
    static void ManageOrders()
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
