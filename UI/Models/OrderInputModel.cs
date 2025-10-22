using System;

namespace CustomerManagement.UI.Models;

public class OrderInputModel
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
