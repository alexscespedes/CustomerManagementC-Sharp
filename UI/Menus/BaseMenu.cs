using System;
using CustomerManagement.UI.Models;
using CustomerManagement.UI.Services;

namespace CustomerManagement.UI.Menus;

public abstract class BaseMenu : IMenu
{
    protected readonly IConsoleService _consoleService;

    protected BaseMenu(IConsoleService consoleService)
    {
        _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
    }

    public abstract void Display();

    protected void ExecuteAction(Func<OperationResult> action)
    {
        try
        {
            var result = action();
            if (result.Success)
            {
                _consoleService.DisplaySuccess(result.Message);
            }
            else
            {
                _consoleService.DisplayError(result.Message);
            }
        }
        catch (Exception ex)
        {
            _consoleService.DisplayError($"An error occurred: {ex.Message}");
        }
        _consoleService.ReadKey();
    }
}
