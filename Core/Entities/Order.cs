namespace CustomerManagement;

public class Order
{
    static int nextId;
    public int OrderId { get; set; }
    public Customer Customer { get; set; }
    public Product Product { get; set; }
    public List<int> Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public List<decimal> TotalAmount { get; set; }
    public Order(Customer customer, Product product, List<int> quantity, DateTime orderDate, List<decimal> totalAmount)
    {
        OrderId = Interlocked.Increment(ref nextId);
        Customer = customer;
        Product = product;
        Quantity = quantity;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
    }

}
