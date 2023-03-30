using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;
public class WaresMenuState : MenuState
{
    public WaresMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _strings = webShopMenu.Strings;
        _optionActions = new Dictionary<int, Action>
        {
            { 1, SeeWares },
            { 2, ShowPurchaseWaresMenu },
            { 3, SortWares },
            {4, LoginOrLogout}
        };
        _options = new List<string>
        {
            _webShopMenu.Strings.Wares.Option1,
            _webShopMenu.Strings.Wares.Option2,
            _webShopMenu.Strings.Wares.Option3,
            _loginState
        };
        CurrentChoice = 1;
    }

    private void LoginOrLogout()
    {
        ((ILoginState)_webShopMenu.LoginState).LoginLogoutHandle();
    }

    private void SortWares()
    {
        ChangeState(StatesEnum.SortMenu);
    }

    private void ShowPurchaseWaresMenu()
    {
        ChangeState(StatesEnum.PurchaseMenu);
    }
    
    private void SeeWares()
    {
        Console.WriteLine();
        foreach (Product product in _webShop.Products)
        {
            product.PrintInfo();
        }
        Console.WriteLine();
    }

    protected internal override void DisplayOptions()
    {
        SetLoginState();
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 4;
        Console.WriteLine(_strings.MenuWhat);
        _webShopMenu.PrintOptions();
    }
}