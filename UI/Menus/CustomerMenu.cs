using System;
using CustomerManagement.UI.Controllers;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Menus;

public class CustomerMenu : BaseMenu
{
    private readonly CustomerController _controller;

    public CustomerMenu(IConsoleService consoleService, CustomerController controller) : base(consoleService)
    {
        _controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    public override void Display()
    {
        while (true)
        {
            _consoleService.Clear();
            _consoleService.WriteLine("=== Customer Management ===");
            _consoleService.WriteLine("1. Add Customer");
            _consoleService.WriteLine("2. View All Customers");
            _consoleService.WriteLine("3. Find Customer by ID");
            _consoleService.WriteLine("4. Update Customer");
            _consoleService.WriteLine("5. Delete Customer");
            _consoleService.WriteLine("6. Search Customer");
            _consoleService.WriteLine("7. Exit");
            _consoleService.Write("Please select an option: ");

            string? choice = _consoleService.ReadLine();

            switch (choice)
            {
                case "1":
                    ExecuteAction(() => _controller.AddCustomer());
                    break;
                case "2":
                    ExecuteAction(() => _controller.ViewAllCustomers());
                    break;
                case "3":
                    ExecuteAction(() => _controller.FindCustomerById());
                    break;
                case "4":
                    ExecuteAction(() => _controller.UpdateCustomer());
                    break;
                case "5":
                    ExecuteAction(() => _controller.DeleteCustomer());
                    break;
                case "6":
                    ExecuteAction(() => _controller.SearchCustomer());
                    break;
                case "7":
                    return;
                default:
                    _consoleService.DisplayError("Invalid option. Try again.");
                    _consoleService.ReadKey();
                    break;
            }
        }
    }
}
