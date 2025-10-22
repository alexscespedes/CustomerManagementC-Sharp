using System;

namespace CustomerManagement.UI.Models;

public class ProductInputModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
