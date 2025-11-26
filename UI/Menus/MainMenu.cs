using System;
using CustomerManagement.UI.Models;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Menus;

public class MainMenu : BaseMenu
{
    private readonly CustomerMenu _customerMenu;
    private readonly ProductMenu _productMenu;
    private readonly OrderMenu _orderMenu;
    private readonly ReportMenu _reportMenu;
    private readonly JsonDataRepository _jsonDataRepository;
    private readonly DataContext _dataContext;

    public MainMenu(
        IConsoleService consoleService,
        CustomerMenu customerMenu,
        ProductMenu productMenu,
        OrderMenu orderMenu,
        ReportMenu reportMenu,
        JsonDataRepository jsonDataRepository,
        DataContext dataContext
        ) : base(consoleService)
    {
        _customerMenu = customerMenu ?? throw new ArgumentNullException(nameof(customerMenu));
        _productMenu = productMenu ?? throw new ArgumentNullException(nameof(productMenu));
        _orderMenu = orderMenu ?? throw new ArgumentNullException(nameof(orderMenu));
        _reportMenu = reportMenu ?? throw new ArgumentNullException(nameof(reportMenu));
        _jsonDataRepository = jsonDataRepository ?? throw new ArgumentNullException(nameof(jsonDataRepository));
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

    }

    public override void Display()
    {
        while (true)
        {
            _consoleService.Clear();
            _consoleService.WriteLine("=== Customer Management System ===");
            _consoleService.WriteLine("1. Manage Customers");
            _consoleService.WriteLine("2. Manage Products");
            _consoleService.WriteLine("3. Manage Orders");
            _consoleService.WriteLine("4. Save Data");
            _consoleService.WriteLine("5. Load Data");
            _consoleService.WriteLine("6. Reports");
            _consoleService.WriteLine("7. Exit");
            _consoleService.Write("Please select an option: ");

            string? choice = _consoleService.ReadLine();

            switch (choice)
            {
                case "1":
                    // _customerMenu.Display();
                    break;
                case "2":

                    break;
                case "3":

                    break;
                case "4":
 
                    break;
                case "5":

                    break;
                case "6":

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

    private OperationResult SaveData()
    {
        try
        {
            _jsonDataRepository.SaveData(_dataContext);
            return OperationResult.SuccessResult("Data saved successfully!");
        }

        catch (Exception ex)
        {
            return OperationResult.FailureResult($"Error saving data: {ex.Message}");
        }
    }

    private OperationResult LoadData()
    {
        try
        {
            _jsonDataRepository.LoadData(_dataContext);
            return OperationResult.SuccessResult("Data loaded successfully!");
        }
        
        catch (Exception ex)
        {
            return OperationResult.FailureResult($"Error loading data: {ex.Message}");
        }
    }
}
