namespace CustomerManagement;

public class Order
{
    private static int nextId = 0;
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public Order() {}
    public Order(int customerId, int productId, int quantity)
    {
        OrderId = Interlocked.Increment(ref nextId);
        CustomerId = customerId;
        ProductId = productId;
        Quantity = quantity;
        OrderDate = DateTime.Now;
    }

    public static void InitializeNextId(int maxExistingId)
    {
        nextId = maxExistingId;
    }

}
