namespace CustomerManagement;

public interface IOrderService
{
    bool CreateOrder(int customerId, int productId, int quantity);
    Order? GetOrderById(int id);
    IEnumerable<Order> GetAllOrders();
    IEnumerable<Order> GetCustomerOrderHistory(int customerId);
    bool DeleteOrder(int id);
    (decimal totalAmount, int orderCount, decimal averageValue) GetSalesReport();
}
