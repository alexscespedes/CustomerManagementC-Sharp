using System;
using CustomerManagement.UI.Models;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Controllers;

public class CustomerController
{
    private readonly ICustomerService _customerService;
    private readonly IConsoleService _consoleService;
    private readonly IInputReader _inputReader;
    private readonly DisplayHelper _displayHelper;

    public CustomerController(ICustomerService customerService, IConsoleService consoleService, IInputReader inputReader, DisplayHelper displayHelper)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
        _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
        _displayHelper = displayHelper ?? throw new ArgumentNullException(nameof(displayHelper));
    }

    public OperationResult AddCustomer()
    {
        string name = _inputReader.ReadString("Enter customer name: ");
        string email = _inputReader.ReadString("Enter customer email: ");
        CustomerType? customerType = _inputReader.ReadCustomerType();

        if (customerType == null)
        {
            return OperationResult.FailureResult("Invalid customer type selected.");
        }

        bool success = _customerService.CreateCustomer(name, email, customerType.Value);
        return success
            ? OperationResult.SuccessResult("Customer created successfully!")
            : OperationResult.FailureResult("Failed to create customer. Check your input.");
    }

    public OperationResult ViewAllCustomers()
    {
        var customers = _customerService.GetAllCustomers();
        _displayHelper.PrintCustomer(customers);
        return OperationResult.SuccessResult();
    }

    public OperationResult FindCustomerById()
    {
        int? customerId = _inputReader.ReadInt("Enter Customer ID: ");
        if (customerId == null)
        {
            return OperationResult.FailureResult("Invalid customer ID.");
        }

        var customer = _customerService.GetCustomerById(customerId.Value);
        if (customer == null)
        {
            return OperationResult.FailureResult("Customer not found");
        }

        _consoleService.WriteLine($"ID: {customer.CustomerId} | Name: {customer.Name} | Name: {customer.Email} | Name: {customer.CustomerType}");
        return OperationResult.SuccessResult();
    }

    public OperationResult UpdateCustomer()
    {
        int? customerId = _inputReader.ReadInt("Enter Customer ID: ");
        if (customerId == null)
        {
            return OperationResult.FailureResult("Invalid customer ID.");
        }

        var existingCustomer = _customerService.GetCustomerById(customerId.Value);
        if (existingCustomer == null)
        {
            return OperationResult.FailureResult("Customer not found.");
        }

        _consoleService.DisplayInfo($"Current: {existingCustomer.Name} | {existingCustomer.Email} | {existingCustomer.CustomerType}");

        string newName = _inputReader.ReadString("Enter new name: ");
        string newEmail = _inputReader.ReadString("Enter new email: ");
        CustomerType? newCustomerType = _inputReader.ReadCustomerType();

        if (newCustomerType == null)
        {
            return OperationResult.FailureResult("Invalid customer type selected.");
        }

        bool success = _customerService.UpdateCustomer(customerId.Value, newName, newEmail, newCustomerType.Value);
        return success
            ? OperationResult.SuccessResult("Customer updated successfully!")
            : OperationResult.FailureResult("Failed to update customer. ");

    }

    public OperationResult DeleteCustomer()
    {
        int? customerId = _inputReader.ReadInt("Enter Customer ID: ");
        if (customerId == null)
        {
            return OperationResult.FailureResult("Invalid customer ID.");
        }

        var customer = _customerService.GetCustomerById(customerId.Value);
        if (customer == null)
        {
            return OperationResult.FailureResult("Customer not found.");
        }

        _consoleService.DisplayInfo($"Customer: [{customer.CustomerId}] | {customer.Name} | {customer.Email} | {customer.CustomerType}");

        if (!_consoleService.GetConfirmation("Are you sure you want to delete this customer?"))
        {
            return OperationResult.FailureResult("Delete operation cancelled.");
        }

        bool success = _customerService.DeleteCustomer(customerId.Value);
        return success
            ? OperationResult.SuccessResult("Customer deleted successfully!")
            : OperationResult.FailureResult("Failed to delete customer. ");
    }

    public OperationResult SearchCustomer()
    {
        string name = _inputReader.ReadString("Enter customer name: ");
        var customers = _customerService.SearchCustomersByName(name);
        _displayHelper.PrintCustomer(customers);
        return OperationResult.SuccessResult();
    }
    
}
