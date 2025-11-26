using System;
using CustomerManagement.UI.Controllers;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Menus;

public class ReportMenu : BaseMenu
{
    private readonly ReportController _controller;
    public ReportMenu(IConsoleService consoleService, ReportController controller) : base(consoleService)
    {
        _controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    public override void Display()
    {
        while (true)
        {
            _consoleService.WriteLine("=== Reports ===");
            _consoleService.WriteLine("1. Total Sales Report");
            _consoleService.WriteLine("2. Customer Order History");
            _consoleService.WriteLine("3. Low Stock Alert");
            _consoleService.WriteLine("4. Exit");
            _consoleService.Write("Please select an option: ");

            string? choice = _consoleService.ReadLine();

            switch (choice)
            {
                case "1":
                    ExecuteAction(() => _controller.TotalSalesReport());
                    break;
                case "2":
                    ExecuteAction(() => _controller.CustomerOrderHistory());
                    break;
                case "3":
                    ExecuteAction(() => _controller.LowStockAlert());
                    break;
                case "4":
                    return;
                default:
                    _consoleService.DisplayError("Invalid option. Try again.");
                    _consoleService.ReadKey();
                    break;
            }
        }
    }
}
