using CustomerManagement.Configuration;

namespace CustomerManagement;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var serviceContainer = ServiceConfiguration.ConfigureServices();

            var MenuManager = serviceContainer.Resolve<MenuManager>();
            MenuManager.MainMenu();

            Console.WriteLine("Thank you for using Customer Management System!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Application error: {ex.Message}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
