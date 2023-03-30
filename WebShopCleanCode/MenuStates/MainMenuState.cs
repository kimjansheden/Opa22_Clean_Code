using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

public class MainMenuState : MenuState
{
    public MainMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _strings = webShopMenu.Strings;
        _optionActions = new Dictionary<int, Action>
        {
            { 1, ShowWaresMenu },
            { 2, ShowCustomerInfo },
            { 3, LoginOrLogout }
        };
        _options = new List<string>
        {
            _webShopMenu.Strings.Main.Option1,
            _webShopMenu.Strings.Main.Option2,
            _loginState
        };
        webShopMenu.CurrentChoice = 1;
    }

    private void LoginOrLogout()
    {
        ((ILoginState)_webShopMenu.LoginState).LoginLogoutHandle();
    }

    private void ShowCustomerInfo()
    {
        ChangeState(StatesEnum.CustomerMenu);
    }

    protected internal override void DisplayOptions()
    {
        SetLoginState();
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 3;
        Console.WriteLine(_strings.MenuWhat);
        _webShopMenu.PrintOptions();
    }

    private void ShowWaresMenu()
    {
        ChangeState(StatesEnum.WaresMenu);
    }
}