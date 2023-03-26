using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class MainMenu : IMenu
{
    private int _amountOfOptions;
    private List<string> _options;
    private int _currentChoice;
    private Strings _strings;
    private WebShopMenu _webShopMenu;
    private WebShop _webShop;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="webShopMenu"></param>
    /// <param name="webShop"></param>
    public MainMenu(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _strings = webShopMenu.Strings;
    }
    public void Run()
    {
        Console.WriteLine(_strings.MainMenuWhat);
        
    }
}