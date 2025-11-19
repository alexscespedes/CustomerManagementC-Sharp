using System;
using CustomerManagement.UI.Models;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Controllers;

public class OrderController
{
    private readonly IOrderService _orderService;
    private readonly IConsoleService _consoleService;
    private readonly IInputReader _inputReader;
    private readonly DisplayHelper _displayHelper;

    public OrderController(IOrderService orderService, IConsoleService consoleService, IInputReader inputReader, DisplayHelper displayHelper)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
        _displayHelper = displayHelper ?? throw new ArgumentNullException(nameof(displayHelper));
    }

    public OperationResult CreateOrder()
    {
        int? customerId = _inputReader.ReadInt("Enter Customer ID: ");
        int? productId = _inputReader.ReadInt("Enter Product ID: ");
        int? quantity = _inputReader.ReadInt("Enter quantity: ");

        if (customerId == null || productId == null || quantity == null)
        {
            return OperationResult.FailureResult("Invalid input. Please enter valid numbers.");
        }

        bool success = _orderService.CreateOrder(customerId.Value, productId.Value, quantity.Value);
        return success
            ? OperationResult.SuccessResult("Order created successfully!")
            : OperationResult.FailureResult("Failed to create order. Check your input.");
    }

    public OperationResult ViewAllOrders()
    {
        var orders = _orderService.GetAllOrders();
        _displayHelper.PrintOrder(orders);
        return OperationResult.SuccessResult();
    }

    public OperationResult FindOrderById()
    {
        int? orderId = _inputReader.ReadInt("Enter Order ID: ");
        if (orderId == null)
        {
            return OperationResult.FailureResult("Invalid order ID.");
        }

        var order = _orderService.GetOrderById(orderId.Value);
        if (order == null)
        {
            return OperationResult.FailureResult("Invalid order ID.");
        }

        _consoleService.WriteLine($"ID: {order.ProductId} | Customer: {order.CustomerId} | Product: {order.ProductId} | Qty: {order.Quantity} | Date: {order.OrderDate:yyyyy--MM-dd} | Total: {order.TotalAmount:C}");
        return OperationResult.SuccessResult();
    }

    public OperationResult DeleteOrder()
    {
        int? orderId = _inputReader.ReadInt("Enter Order ID: ");
        if (orderId == null)
        {
            return OperationResult.FailureResult("Invalid order ID.");
        }

        var order = _orderService.GetOrderById(orderId.Value);
        if (order == null)
        {
            return OperationResult.FailureResult("Order not found");
        }

        _consoleService.DisplayInfo($"Order: [{order.OrderId}] Qty: {order.Quantity} | Total: {order.TotalAmount:C} | Date: {order.OrderDate:yyyy-MM-dd}");

        if (!_consoleService.GetConfirmation("Are you sure you want to delete this order?"))
        {
            return OperationResult.FailureResult("Delete operation cancelled.");
        }

        bool success = _orderService.DeleteOrder(orderId.Value);
        return success
            ? OperationResult.SuccessResult("Order deleted successfully!")
            : OperationResult.FailureResult("Failed to delete order. ");
    }
}
