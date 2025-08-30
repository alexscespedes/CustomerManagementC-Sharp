using System;

namespace CustomerManagement.Repositories;

public interface IProductRepository
{
    void Add(Product product);
    Product? GetById(int id);
    IEnumerable<Product> GetAll();
    IEnumerable<Product> SearchByName(string name);
    IEnumerable<Product> GetLowStockProducts(int threshold = 5);
    void Update(Product product);
    void Remove(Product product);
}
