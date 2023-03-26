using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class WaresMenu : IMenu
{
    private int _amountOfOptions;
    private List<string> _options;
    private int _currentChoice;
    private readonly WebShop _webShop;
    private readonly WebShopMenu _webShopMenu;
    private Strings _strings;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="webShop"></param>
    /// <param name="webShopMenu"></param>
    public WaresMenu(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;
    }
    public void Run()
    {
        // Coming from Main menu (OkCommand Line 21)
        _webShopMenu.Options[0] = "See all wares";
        _webShopMenu.Options[1] = "Purchase a ware";
        _webShopMenu.Options[2] = "Sort wares";
        if (_webShop.currentCustomer == null)
        {
            _webShopMenu.Options[3] = "Login";
        }
        else
        {
            _webShopMenu.Options[3] = "Logout";
        }
        _webShopMenu.AmountOfOptions = 4;
        _strings.currentMenu = "wares menu";
        _strings.MainMenuWhat = "What would you like to do?";
    }
}