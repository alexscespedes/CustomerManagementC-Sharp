
using CustomerManagement.Repositories;

namespace CustomerManagement;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private const decimal PREMIUN_DISCOUNT_RATE = 0.10m;

    public OrderService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository
    )
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public bool CreateOrder(int customerId, int productId, int quantity)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            return false;

        var product = _productRepository.GetById(productId);
        if (product == null)
            return false;

        if (quantity <= 0)
            return false;

        if (product.StockQuantity < quantity)
            return false;

        var order = new Order(customerId, productId, quantity);

        decimal subtotal = quantity * product.Price;
        if (customer.CustomerType == CustomerType.Premium)
        {
            decimal discount = subtotal * PREMIUN_DISCOUNT_RATE;
            order.TotalAmount = subtotal - discount;
        }
        else
        {
            order.TotalAmount = subtotal;
        }

        product.StockQuantity -= quantity;
        _productRepository.Update(product);

        _orderRepository.Add(order);
        return true;

    }

    public bool DeleteOrder(int id)
    {
        var order = _orderRepository.GetById(id);
        if (order == null)
            return false;

        _orderRepository.Remove(order);
        return true;
    }

    public IEnumerable<Order> GetAllOrders()
    {
        return _orderRepository.GetAll();
    }

    public IEnumerable<Order> GetCustomerOrderHistory(int customerId)
    {
        return _orderRepository.GetByCustomerId(customerId);
    }

    public Order? GetOrderById(int id)
    {
        return _orderRepository.GetById(id);
    }

    public (decimal totalAmount, int orderCount, decimal averageValue) GetSalesReport()
    {
        return (
            _orderRepository.GetTotalSalesAmount(),
            _orderRepository.GetTotalOrderCount(),
            _orderRepository.GetAverageOrderValue()
        );
    }
}
