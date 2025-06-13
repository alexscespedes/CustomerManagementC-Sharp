namespace CustomerManagement;

public class Order
{
    static int nextId;
    public int OrderId { get; set; }
    public Customer Customer { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public Order(Customer customer, Product product, int quantity, List<decimal> totalAmount)
    {
        OrderId = Interlocked.Increment(ref nextId);
        Customer = customer;
        Product = product;
        Quantity = quantity;
        OrderDate = DateTime.Now;
        TotalAmount = Quantity * Product.Price;
    }

}
