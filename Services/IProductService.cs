namespace CustomerManagement;

public interface IProductService
{
    bool CreateProduct(string name, decimal price, int stockQuantity);
    Product? GetProductById(int id);
    IEnumerable<Product> GetAllProducts();
    IEnumerable<Product> SearchProductByName(string name);
    bool UpdateProduct(int id, string name, decimal price, int stockQuantity);
    bool DeleteProduct(int id);
    IEnumerable<Product> GetLowStockProducts(int threshold = 5);
    decimal CalculateLowStockValue(int threshold = 5);
}
