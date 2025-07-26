using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CustomerManagement;

public class JsonDataRepository
{
    string filePath = "/home/alexsc03/Documents/CSharpConsoleApps/CustomerManagementC-Sharp/json/";
    JsonSerializerOptions options = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, MaxDepth = 256, WriteIndented = true };

    public void SaveData(DataContext dataContext)
    {
        try
        {
            string customerJson = JsonSerializer.Serialize(dataContext.Customers, options);
            File.WriteAllText(filePath + "customers.json", customerJson);

            string productJson = JsonSerializer.Serialize(dataContext.Products, options);
            File.WriteAllText(filePath + "products.json", productJson);

            string orderJson = JsonSerializer.Serialize(dataContext.Orders, options);
            File.WriteAllText(filePath + "orders.json", orderJson);

            Console.WriteLine("Data successfully saved");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Saving JSON failed: {ex.Message}");
        }
    }

    public void LoadData(DataContext dataContext)
    {
        try
        {
            var customerFilePath = Path.Combine(filePath, "customers.json");
            var productFilePath = Path.Combine(filePath, "products.json");
            var orderFilePath = Path.Combine(filePath, "orders.json");

            if (File.Exists(customerFilePath))
            {
                string customerJsonFile = File.ReadAllText(customerFilePath);

                var customerList = JsonSerializer.Deserialize<List<Customer>>(customerJsonFile);

                if (customerList != null && customerList.Count > 0)
                {
                    dataContext.Customers = customerList!;

                    var maxId = customerList.Max(c => c.CustomerId);
                    Customer.InitializeNextId(maxId);
                }
            }

            if (File.Exists(productFilePath))
            {
                string productJsonFile = File.ReadAllText(productFilePath);

                var productList = JsonSerializer.Deserialize<List<Product>>(productJsonFile);

                if (productList != null && productList.Count > 0)
                {
                    dataContext.Products = productList!;

                    var maxId = productList.Max(c => c.ProductId);
                    Product.InitializeNextId(maxId);
                }
            }

            if (File.Exists(orderFilePath))
            {
                string orderJsonFile = File.ReadAllText(orderFilePath);

                var orderList = JsonSerializer.Deserialize<List<Order>>(orderJsonFile);

                if (orderList != null && orderList.Count > 0)
                {
                    dataContext.Orders = orderList!;

                    var maxId = orderList.Max(c => c.OrderId);
                    Order.InitializeNextId(maxId);
                }
            }

            Console.WriteLine("Data successfully loaded");
            Console.ReadLine();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");

            dataContext.Customers = new List<Customer>();
            dataContext.Products = new List<Product>();
            dataContext.Orders = new List<Order>();
        }
    }
}
