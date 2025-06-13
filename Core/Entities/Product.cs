namespace CustomerManagement;

public class Product
{
    static int nextId;
    public int ProductId { get; private set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public Product(string name, decimal price, int stockQuantity)
    {
        ProductId = Interlocked.Increment(ref nextId);
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
    }
}
