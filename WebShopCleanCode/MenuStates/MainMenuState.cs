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
        SetLoginState();
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
        _webShopMenu.AmountOfOptions = 3;
    }
    private void SetLoginState()
    {
        _loginState = _webShop.LoginState is LoggedInState ? _strings.LoginString : _strings.LogoutString;
    }

    private void LoginOrLogout()
    {
        if (_webShop.LoginState is LoggedOutState)
        {
            _options.Clear();
            _options.AddRange(new string[] {_strings.Login.Option1, _strings.Login.Option2, _strings.Login.Option3, _strings.Login.Option4});
            _webShopMenu.AmountOfOptions = 4;
            _webShopMenu.CurrentChoice = 1;
            _strings.MainMenuWhat = "Please submit username and password.";
            _webShopMenu.Username = null;
            _webShopMenu.Password = null;
            _webShopMenu.CurrentMenu = "login menu";
            // Nä denna måste till LoginMenuState och ha fyra nya actions ju: 1. Set username, 2. Set password, 3. Login och 4. Register.
            _optionActions.Add(4, Register);
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

    private void Register()
    {
        throw new NotImplementedException();
    }

    private void ShowCustomerInfo()
    {
        throw new NotImplementedException();
    }

    public void DisplayOptions()
    {
        SetLoginState();
        _webShopMenu.SetOptions(_options);
        _webShopMenu.CurrentMenu = _webShopMenu.Strings.MainMenu;
        _webShopMenu.PrintOptions();
    }

    public void ExecuteOption(int option)
    {
        _optionActions[option]();
    }

    private void ShowWaresMenu()
    {
        _webShopMenu.CurrentState = new WaresMenuState(_webShopMenu, _webShop);
    }
}