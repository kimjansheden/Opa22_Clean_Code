using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

public class LoggedOutState : ILoginState
{
    private readonly WebShop _webShop;
    private Strings _strings;
    private readonly WebShopMenu _webShopMenu;
    public LoggedOutState(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;
    }
    public void RequestHandle()
    {
        Console.WriteLine();
        Console.WriteLine("You must be logged in to purchase wares.");
        Console.WriteLine();
        // _webShopMenu.CurrentChoice = 1;
    }
}