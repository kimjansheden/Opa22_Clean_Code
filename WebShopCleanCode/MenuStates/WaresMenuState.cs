using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode.MenuStates;
public class WaresMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _webShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginMessage;
    private Strings _strings;
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
        SetLoginMessage();
    }

    private void LoginOrLogout()
    {
        if (_webShop.LoginState is LoggedInState)
        {
            _webShopMenu.Options[3] = "Login";
            Console.WriteLine();
            Console.WriteLine(_webShop.currentCustomer.Username + " logged out.");
            Console.WriteLine();
            _webShop.currentCustomer = null;
            _webShopMenu.CurrentChoice = 1;
        }
        else if (_webShop.LoginState is LoggedOutState)
        {
            {
                _webShopMenu.Options[0] = "Set Username";
                _webShopMenu.Options[1] = "Set Password";
                _webShopMenu.Options[2] = "Login";
                _webShopMenu.Options[3] = "Register";
                _webShopMenu.AmountOfOptions = 4;
                _strings.MainMenuWhat = "Please submit username and password.";
                _webShopMenu.CurrentChoice = 1;
                _webShopMenu.CurrentMenu = "login menu";
            }
        }
    }

    private void SetLoginMessage()
    {
        _loginMessage = _webShop.LoginState is LoggedInState ? _strings.LoginString : _strings.LogoutString;
    }

    private void SortWares()
    {
        _webShopMenu.Options[0] = "Sort by name, descending";
        _webShopMenu.Options[1] = "Sort by name, ascending";
        _webShopMenu.Options[2] = "Sort by price, descending";
        _webShopMenu.Options[3] = "Sort by price, ascending";
        _strings.MainMenuWhat = "How would you like to sort them?";
        _webShopMenu.CurrentMenu = "sort menu";
        _webShopMenu.CurrentChoice = 1;
        _webShopMenu.AmountOfOptions = 4;
    }

    private void ShowPurchaseWaresMenu()
    {
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
        SetLoginMessage();
        _webShopMenu.SetOptions(new List<string>
        {
            _webShopMenu.Strings.Wares.Option1,
            _webShopMenu.Strings.Wares.Option2,
            _webShopMenu.Strings.Wares.Option3,
            _loginMessage
        });
        _webShopMenu.AmountOfOptions = 4;
        _webShopMenu.CurrentMenu = _strings.WaresMenu;
        _webShopMenu.PrintOptions();
        //_strings.MainMenuWhat = "What would you like to do?";
    }

    public void ExecuteOption(int option)
    {
        _optionActions[option]();
    }
}