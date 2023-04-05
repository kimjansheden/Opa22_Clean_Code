using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Helpers;

public class MenuManager : IMenuManager
{
    private readonly IApp _app;

    public MenuManager(IApp app)
    {
        _app = app;
    }

    public void PrintNavigation()
    {
        for (int i = 0; i < _app.AmountOfOptions; i++)
        {
            Console.Write(i + 1 + "\t");
        }
        Console.WriteLine();
        for (int i = 1; i < _app.CurrentChoice; i++)
        {
            Console.Write("\t");
        }
        Console.WriteLine("|");

        Console.WriteLine(_app.Strings.Buttons);
        DisplayUser(_app.CurrentCustomer);
    }

    private void DisplayUser(Customer customer) => Console.WriteLine(customer != null ? $"Current user: {customer.Username}" : "Nobody logged in.");
}