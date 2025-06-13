namespace CustomerManagement;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome, Alexander!");
        MenuManager menuManager = new MenuManager();
        
        menuManager.MainMenu();

    }
}
