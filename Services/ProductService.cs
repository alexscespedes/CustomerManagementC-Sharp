
using CustomerManagement.Repositories;

namespace CustomerManagement;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public bool CreateProduct(string name, decimal price, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (price <= 0)
            return false;

        if (stockQuantity < 0)
            return false;

        var product = new Product(name, price, stockQuantity);
        _productRepository.Add(product);
        return true;
    }

    public bool DeleteProduct(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
            return false;

        _productRepository.Remove(product);
        return true;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _productRepository.GetAll();
    }

    public IEnumerable<Product> GetLowStockProducts(int threshold = 5)
    {
        return _productRepository.GetLowStockProducts(threshold);
    }

    public Product? GetProductById(int id)
    {
        return _productRepository.GetById(id);
    }

    public IEnumerable<Product> SearchProductsByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Enumerable.Empty<Product>();

        return _productRepository.SearchByName(name);
    }

    public bool UpdateProduct(int id, string name, decimal price, int stockQuantity)
    {
        var existingProduct = _productRepository.GetById(id);
        if (existingProduct == null)
            return false;

        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (price <= 0)
            return false;

        if (stockQuantity < 0)
            return false;

        existingProduct.Name = name;
        existingProduct.Price = price;
        existingProduct.StockQuantity = stockQuantity;
        _productRepository.Update(existingProduct);
        return true;
    }

    public decimal CalculateLowStockValue(int threshold = 5)
    {
        var lowStockProducts = _productRepository.GetLowStockProducts(threshold);
        return lowStockProducts.Sum(p => p.Price * p.StockQuantity);
    }
}