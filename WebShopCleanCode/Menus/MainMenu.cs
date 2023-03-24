using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class MainMenu : IMenu
{
    private int _amountOfOptions;
    private List<string> _options;
    private int _currentChoice;
    private Strings _strings;

    public MainMenu(WebShopMenu webShopMenu)
    {
        _strings = webShopMenu.Strings;
    }
    public void Run()
    {
        Console.WriteLine(_strings.MainMenuWhat);
    }
    
    
}