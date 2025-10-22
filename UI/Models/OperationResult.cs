using System;

namespace CustomerManagement.UI.Models;

public class OperationResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }

    public static OperationResult SuccessResult(string message = "Operation completed successfully", object? data = null)
    {
        return new OperationResult
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static OperationResult FailureResult(string message)
    {
        return new OperationResult
        {
            Success = false,
            Message = message
        };
    }
}