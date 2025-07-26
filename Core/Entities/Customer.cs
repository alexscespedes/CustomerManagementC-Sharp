namespace CustomerManagement;

public class Customer
{
    private static int nextId = 0;
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public CustomerType CustomerType { get; set; }

    public Customer() { }
    public Customer(string name, string email, CustomerType customerType)
    {
        CustomerId = Interlocked.Increment(ref nextId);
        Name = name;
        Email = email;
        CustomerType = customerType;
    }
    
    public static void InitializeNextId(int maxExistingId)
    {
        nextId = maxExistingId;
    }
}
