using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

public class MainMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _webShop;
    private string _loginState;
    private Strings _strings;
    private Dictionary<int, Action> _optionActions;
    private List<string> _options;

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
    private void SetLoginState()
    {
        _loginState = _webShop.LoginState is LoggedInState ? _strings.LogoutString : _strings.LoginString;
        _options[_options.FindIndex(o => o == null || o == _strings.LogoutString || o == _strings.LoginString)] = _loginState;
    }

    private void LoginOrLogout()
    {
        if (_webShop.LoginState is LoggedOutState)
        {
            _webShopMenu.PreviousMenuState = this;
            _webShopMenu.CurrentState = new LoginMenuState(_webShopMenu, _webShop);
        }
        else if (_webShop.LoginState is LoggedInState)
        {
            _options[2] = "Login";
            Console.WriteLine();
            Console.WriteLine(_webShop.currentCustomer.Username + " logged out.");
            Console.WriteLine();
            _webShopMenu.CurrentChoice = 1;
            _webShop.currentCustomer = null;
            _webShop.LoginState = new LoggedOutState(_webShop, _webShopMenu);
            _webShopMenu.AmountOfOptions = 3;
        }
    }

    private void ShowCustomerInfo()
    {
        _webShopMenu.PreviousMenuState = this;
        _webShopMenu.CurrentState = new CustomerInfoMenuState(_webShopMenu, _webShop);
    }

    public void DisplayOptions()
    {
        SetLoginState();
        _webShopMenu.SetOptions(_options);
        //_webShopMenu.CurrentMenu = _webShopMenu.Strings.MainMenu;
        _webShopMenu.AmountOfOptions = 3;
        Console.WriteLine(_strings.MenuWhat);
        _webShopMenu.PrintOptions();
    }

    public void ExecuteOption(int option)
    {
        _optionActions[option]();
    }

    private void ShowWaresMenu()
    {
        _webShopMenu.PreviousMenuState = this;
        _webShopMenu.CurrentState = new WaresMenuState(_webShopMenu, _webShop);
    }
}