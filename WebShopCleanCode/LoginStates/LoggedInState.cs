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
        _webShopMenu.CurrentMenu = "purchase menu";
        Console.WriteLine("What would you like to purchase?");
        //_strings.MainMenuWhat = "What would you like to purchase?";
        //_webShopMenu.CurrentChoice = 1;
        _webShopMenu.AmountOfOptions = _webShop.products.Count;
    }
}