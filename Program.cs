using CustomerManagement.Configuration;
using CustomerManagement.UI.Menus;

namespace CustomerManagement;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var serviceContainer = ServiceConfiguration.ConfigureServices();

            var mainMenu = serviceContainer.Resolve<MainMenu>();
            mainMenu.Display();

            Console.WriteLine("\nThank you for using Customer Management System!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Application error: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
