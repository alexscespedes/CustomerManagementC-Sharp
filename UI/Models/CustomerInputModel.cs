using System;

namespace CustomerManagement.UI.Models;

public class CustomerInputModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public CustomerType CustomerType { get; set; }
}
