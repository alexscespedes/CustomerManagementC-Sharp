using System;
using CustomerManagement.UI.Controllers;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Menus;

public class ProductMenu : BaseMenu
{
    private readonly ProductController _controller;
    public ProductMenu(IConsoleService consoleService, ProductController controller) : base(consoleService)
    {
        _controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    public override void Display()
    {
        while (true)
        {
            _consoleService.WriteLine("=== Product Management ===");
            _consoleService.WriteLine("1. Add Product");
            _consoleService.WriteLine("2. View All Products");
            _consoleService.WriteLine("3. Find Product by ID");
            _consoleService.WriteLine("4. Update Product");
            _consoleService.WriteLine("5. Delete Product");
            _consoleService.WriteLine("6. Search Product");
            _consoleService.WriteLine("7. Exit");
            _consoleService.Write("Please select an option: ");

            string? choice = _consoleService.ReadLine();

            switch (choice)
            {
                case "1":
                    ExecuteAction(() => _controller.AddProduct());
                    break;
                case "2":
                    ExecuteAction(() => _controller.ViewAllProducts());
                    break;
                case "3":
                    ExecuteAction(() => _controller.FindProductById());
                    break;
                case "4":
                    ExecuteAction(() => _controller.UpdateProduct());
                    break;
                case "5":
                    ExecuteAction(() => _controller.DeleteProduct());
                    break;
                case "6":
                    ExecuteAction(() => _controller.SearchProduct());
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
}
