namespace CustomerManagement;

public class Customer
{
    static int nextId;
    public int CustomerId { get; private set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public CustomerType CustomerType { get; set; }
    public Customer(string name, string email, CustomerType customerType)
    {
        CustomerId = Interlocked.Increment(ref nextId);
        Name = name;
        Email = email;
        CustomerType = customerType;
    }

}
