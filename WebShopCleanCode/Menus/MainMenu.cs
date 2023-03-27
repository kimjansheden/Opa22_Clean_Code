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
        _webShopMenu.Options[0] = _strings.Main.Option1;
        _webShopMenu.Options[1] = _strings.Main.Option2;
        _webShopMenu.Options[2] = _strings.Main.Option3;
        _webShopMenu.AmountOfOptions = 3;
        _webShopMenu.CurrentMenu = _strings.MainMenu;
    }
}