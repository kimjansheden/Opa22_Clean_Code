using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode.MenuStates;
public class WaresMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _webShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginState;
    private Strings _strings;
    private List<string> _options;
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
        webShopMenu.CurrentChoice = 1;
    }

    private void LoginOrLogout()
    {
        if (_webShop.LoginState is LoggedInState)
        {
            _options[3] = _strings.LoginString;
            Console.WriteLine();
            Console.WriteLine(_webShop.currentCustomer.Username + " logged out.");
            Console.WriteLine();
            _webShop.currentCustomer = null;
            _webShop.LoginState = new LoggedOutState(_webShop, _webShopMenu);
            _webShopMenu.CurrentChoice = 1;
        }
        else if (_webShop.LoginState is LoggedOutState)
        {
            {
                _webShopMenu.PreviousMenuState = this;
                _webShopMenu.CurrentState = new LoginMenuState(_webShopMenu, _webShop);
            }
        }
    }

    // Den här skulle kunna vara gemensam. Kanske ändra om IMenuState till abstract så alla kan dela på den?
    private void SetLoginState()
    {
        _loginState = _webShop.LoginState is LoggedInState ? _strings.LogoutString : _strings.LoginString;
        _options[_options.FindIndex(o => o == null || o == _strings.LogoutString || o == _strings.LoginString)] = _loginState;
    }

    private void SortWares()
    {
        _webShopMenu.PreviousMenuState = this;
        _webShopMenu.CurrentState = new SortMenuState(_webShopMenu, _webShop);
    }

    private void ShowPurchaseWaresMenu()
    {
        _webShopMenu.PreviousMenuState = this;
        _webShopMenu.CurrentState = new PurchaseMenuState(_webShopMenu, _webShop);
    }

    private void SeeWares()
    {
        Console.WriteLine();
        foreach (Product product in _webShop.products)
        {
            product.PrintInfo();
        }
        Console.WriteLine();
    }

    public void DisplayOptions()
    {
        SetLoginState();
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 4;
        //_webShopMenu.CurrentMenu = _strings.WaresMenu;
        Console.WriteLine(_strings.MenuWhat);
        _webShopMenu.PrintOptions();
    }

    public void ExecuteOption(int option)
    {
        _optionActions[option]();
    }
}