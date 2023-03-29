using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

public class LoggedInState : ILoginState
{
    private readonly WebShop _webShop;
    private Strings _strings;
    private readonly WebShopMenu _webShopMenu;

    public LoggedInState(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;
    }

    public void RequestHandle()
    {
        Console.WriteLine("What would you like to purchase?");
        _webShopMenu.AmountOfOptions = _webShop.products.Count;
    }
}