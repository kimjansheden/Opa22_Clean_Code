using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.LoginStates;

namespace WebShopCleanCode.MenuStates;

public class MainMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _defaultWebShop;
    private string _loginState;
    private Strings _strings;
    private Dictionary<int, Action> _optionActions;
    private List<string> _options;

    public MainMenuState(WebShopMenu webShopMenu, WebShop defaultWebShop)
    {
        _webShopMenu = webShopMenu;
        _defaultWebShop = defaultWebShop;
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
    public IState CurrentState
    {
        get => _webShopMenu.CurrentState;
        set => _webShopMenu.CurrentState = value;
    }

    public IState PreviousState
    {
        get => _webShopMenu.PreviousState;
        set => _webShopMenu.PreviousState = value;
    }

    public Dictionary<StatesEnum, IMenuState> States
    {
        get => _webShopMenu.States;
        set => _webShopMenu.States = value;
    }
    public int CurrentChoice
    {
        get => _webShopMenu.CurrentChoice;
        set => _webShopMenu.CurrentChoice = value;
    }
    public List<IState> StateHistory
    {
        get => _webShopMenu.StateHistory;
        set => _webShopMenu.StateHistory = value;
    }
    private void SetLoginState()
    {
        _loginState = _webShopMenu.LoginState is LoggedInState ? _strings.LogoutString : _strings.LoginString;
        _options[_options.FindIndex(o => o == null || o == _strings.LogoutString || o == _strings.LoginString)] = _loginState;
    }

    private void LoginOrLogout()
    {
        if (_webShopMenu.LoginState is LoggedOutState)
        {
            ChangeState(StatesEnum.LoginMenu);
        }
        else if (_webShopMenu.LoginState is LoggedInState)
        {
            _options[2] = _strings.LoginString;
            Console.WriteLine();
            Console.WriteLine(_defaultWebShop.CurrentCustomer.Username + " logged out.");
            Console.WriteLine();
            CurrentChoice = 1;
            _defaultWebShop.CurrentCustomer = null;
            _webShopMenu.LoginState = new LoggedOutState(_webShopMenu);
            _webShopMenu.AmountOfOptions = 3;
        }
    }

    private void ShowCustomerInfo()
    {
        ChangeState(StatesEnum.CustomerMenu);
    }

    public void DisplayOptions()
    {
        SetLoginState();
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 3;
        Console.WriteLine(_strings.MenuWhat);
        _webShopMenu.PrintOptions();
    }

    public void ExecuteOption(int option)
    {
        _optionActions[option]();
    }

    public void ChangeState(StatesEnum stateEnum)
    {
        PreviousState = this;
        CurrentState = States[stateEnum];
        CurrentChoice = 1;
        StateHistory.Add(this);
    }

    private void ShowWaresMenu()
    {
        ChangeState(StatesEnum.WaresMenu);
    }
}