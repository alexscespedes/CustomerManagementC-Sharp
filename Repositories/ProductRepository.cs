using System;

namespace CustomerManagement.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _dataContext;

    public ProductRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public void Add(Product product)
    {
        _dataContext.Products.Add(product);
    }

    public IEnumerable<Product> GetAll()
    {
        return _dataContext.Products.ToList();
    }

    public Product? GetById(int id)
    {
        return _dataContext.Products.FirstOrDefault(p => p.ProductId == id);
    }

    public IEnumerable<Product> GetLowStockProducts(int threshold = 5)
    {
        return _dataContext.Products.Where(p => p.StockQuantity < threshold).ToList();
    }

    public void Remove(Product product)
    {
        _dataContext.Products.Remove(product);
    }

    public IEnumerable<Product> SearchByName(string name)
    {
        return _dataContext.Products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void Update(Product product)
    {
        var existingProduct = GetById(product.ProductId);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
        }
    }
}
