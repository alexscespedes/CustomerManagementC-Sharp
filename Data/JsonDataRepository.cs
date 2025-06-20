using System.Text.Json;
using System.Text.Json.Serialization;

namespace CustomerManagement;

public class JsonDataRepository
{
    string filePath = "/home/alexsc03/Documents/Projects/DotNet/C-SharpConsoleApps/CustomerManagementSystem/json/";
    JsonSerializerOptions options = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, MaxDepth = 256, WriteIndented = true };

    public void SaveData(DataContext _)
    {
        try
        {
            string customerJson = JsonSerializer.Serialize(DataContext.Customers, options);
            File.WriteAllText(filePath + "customers.json", customerJson);

            string productJson = JsonSerializer.Serialize(DataContext.Products, options);
            File.WriteAllText(filePath + "products.json", productJson);

            string orderJson = JsonSerializer.Serialize(DataContext.Orders, options);
            File.WriteAllText(filePath + "orders.json", orderJson);

            Console.WriteLine("Data successfully saved");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Saving JSON failed: {ex.Message}");
        }
    }

    public void LoadData(DataContext _)
    {
        try
        {
            string customerJsonFile = File.ReadAllText(filePath + "customers.json");
            var customerList = JsonSerializer.Deserialize<List<Customer>>(customerJsonFile);
            if (customerList == null && customerList?.Count == 0)
            {
                Console.WriteLine($"The customer list is null or empty");
            }
            DataContext.Customers = customerList!;

            string productJsonFile = File.ReadAllText(filePath + "products.json");
            var productList = JsonSerializer.Deserialize<List<Product>>(productJsonFile);
            if (productList == null && productList?.Count == 0)
            {
                Console.WriteLine($"The product list is null or empty");
            }
            DataContext.Products = productList!;

            string orderJsonFile = File.ReadAllText(filePath + "orders.json");
            var orderList = JsonSerializer.Deserialize<List<Order>>(orderJsonFile);
            if (orderList == null && orderList?.Count == 0)
            {
                Console.WriteLine($"The order list is null or empty");
            }
            DataContext.Orders = orderList!;

            Console.WriteLine("Data successfully loaded");
            Console.ReadLine();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Loading JSON failed: {ex.Message}");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Json File not found: {ex.Message}");
        }
    }


}
