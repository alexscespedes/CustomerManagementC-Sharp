using System;
using CustomerManagement.UI.Models;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Controllers;

public class ReportController
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IConsoleService _consoleService;
    private readonly IInputReader _inputReader;
    private readonly DisplayHelper _displayHelper;

    public ReportController(
        IOrderService orderService,
        IProductService productService,
        IConsoleService consoleService,
        IInputReader inputReader,
        DisplayHelper displayHelper)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
        _displayHelper = displayHelper ?? throw new ArgumentNullException(nameof(displayHelper));
    }

    public  OperationResult TotalSalesReport()
    {
        var (totalAmount, orderCount, averageValue) = _orderService.GetSalesReport();

        _consoleService.WriteLine("=== Total Sales Report ===");
        _consoleService.WriteLine($"Total Sales Amount: {totalAmount:C}");
        _consoleService.WriteLine($"Total Number of Orders: {orderCount}");
        _consoleService.WriteLine($"Average Order Value: {averageValue:C}");

        return OperationResult.SuccessResult();
    }

    public OperationResult CustomerOrderHistory()
    {
        int? customerId = _inputReader.ReadInt("Enter Customer ID: ");
        if (customerId == null)
        {
            return OperationResult.FailureResult("Invalid customer ID.");
        }

        var orders = _orderService.GetCustomerOrderHistory(customerId.Value);
        if (!orders.Any())
        {
            _consoleService.DisplayInfo("No orders found for this customer.");
        }
        else
        {
            _consoleService.WriteLine($"=== Order History for Customer ID: {customerId.Value} ===");
            _displayHelper.PrintOrder(orders);
        }

        return OperationResult.SuccessResult();
    }

    public OperationResult LowStockAlert()
    {
        var lowStockProducts = _productService.GetLowStockProducts();
        var totalValue = _productService.CalculateLowStockValue();

        _consoleService.WriteLine("=== Low Stock Alert (Less than 5 items) ===");

        if (!lowStockProducts.Any())
        {
            _consoleService.DisplayInfo("No products with low stock.");
        }
        else
        {
            _displayHelper.PrintProduct(lowStockProducts);
            _consoleService.WriteLine($"--- Total Value of Low Stock Products: {totalValue:C} ---");
        }

        return OperationResult.SuccessResult();
    }
}
