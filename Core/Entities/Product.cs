namespace CustomerManagement;

public class Product
{
    private static int nextId = 0;
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public Product() { }

    public Product(string name, decimal price, int stockQuantity)
    {
        ProductId = Interlocked.Increment(ref nextId);
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public static void InitializeNextId(int maxExistingId)
    {
        nextId = maxExistingId;
    }
}
