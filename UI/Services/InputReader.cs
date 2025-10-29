using System;

namespace CustomerManagement.UI.Services;

public class InputReader : IInputReader
{
    private readonly IConsoleService _consoleService;

    public InputReader(IConsoleService consoleService)
    {
        _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
    }

    public CustomerType? ReadCustomerType()
    {
        _consoleService.WriteLine("Select customer type:");
        _consoleService.WriteLine("1. Regular");
        _consoleService.WriteLine("2. Premiun");
        _consoleService.Write("Choose an option: ");

        if (int.TryParse(_consoleService.ReadLine(), out int choice))
        {
            return choice switch
            {
                1 => CustomerType.Regular,
                2 => CustomerType.Premium,
                _ => null
            };
        }
        return null;
    }

    public decimal? ReadDecimal(string prompt)
    {
        _consoleService.Write(prompt);
        if (decimal.TryParse(_consoleService.ReadLine(), out decimal result))
        {
            return result;
        }
        return null;
    }

    public int? ReadInt(string prompt)
    {
        _consoleService.Write(prompt);
        if (int.TryParse(_consoleService.ReadLine(), out int result))
        {
            return result;
        }
        return null;
    }

    public string ReadString(string prompt)
    {
        _consoleService.Write(prompt);
        return _consoleService.ReadLine() ?? string.Empty;
    }
}
