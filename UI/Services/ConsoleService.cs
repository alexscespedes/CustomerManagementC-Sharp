using System;

namespace CustomerManagement.UI.Services;

public class ConsoleService : IConsoleService
{
    public void Clear() => Console.Clear();

    public void DisplayError(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✗ {message}");
        Console.ForegroundColor = originalColor;
    }

    public void DisplayInfo(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"ℹ {message}");
        Console.ForegroundColor = originalColor;
    }

    public void DisplaySuccess(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✓ {message}");
        Console.ForegroundColor = originalColor;
    }

    public bool GetConfirmation(string message)
    {
        Write($"{message} (y/n): ");
        string? response = ReadLine();
        return response?.ToLower() == "y" || response?.ToLower() == "yes";
    }

    public void ReadKey() => Console.ReadKey();

    public string? ReadLine() => Console.ReadLine();

    public void Write(string message) => Console.Write(message);

    public void WriteLine(string message) => Console.WriteLine(message);
}
