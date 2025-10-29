using System;

namespace CustomerManagement.UI.Services;

public interface IInputReader
{
    string ReadString(string prompt);
    int? ReadInt(string prompt);
    decimal? ReadDecimal(string prompt);
    CustomerType? ReadCustomerType();
}
