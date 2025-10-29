using System;

namespace CustomerManagement.UI.Services;

public interface IConsoleService
{
    void WriteLine(string message);
    void Write(string message);
    string? ReadLine();
    void Clear();
    void ReadKey();
    void DisplaySuccess(string message);
    void DisplayError(string message);
    void DisplayInfo(string message);
    bool GetConfirmation(string message);
}
