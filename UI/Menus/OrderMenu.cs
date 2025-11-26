using System;
using CustomerManagement.UI.Controllers;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Menus;

public class OrderMenu : BaseMenu
{
    private readonly OrderController _controller;
    public OrderMenu(IConsoleService consoleService, OrderController controller) : base(consoleService)
    {
        _controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    public override void Display()
    {
        while (true)
        {
            _consoleService.WriteLine("=== Order Management ===");
            _consoleService.WriteLine("1. Create Order");
            _consoleService.WriteLine("2. View All Orders");
            _consoleService.WriteLine("3. Find Order by ID");
            _consoleService.WriteLine("4. Delete Order");
            _consoleService.WriteLine("5. Exit");
            _consoleService.Write("Please select an option: ");

            string? choice = _consoleService.ReadLine();

            switch (choice)
            {
                case "1":
                    ExecuteAction(() => _controller.CreateOrder());
                    break;
                case "2":
                    ExecuteAction(() => _controller.ViewAllOrders());
                    break;
                case "3":
                    ExecuteAction(() => _controller.FindOrderById());
                    break;
                case "4":
                    ExecuteAction(() => _controller.DeleteOrder());
                    break;
                case "5":
                    return;
                default:
                    _consoleService.WriteLine("Invalid option. Try again.");
                    _consoleService.ReadKey();
                    break;
            }
        }
    }
}
