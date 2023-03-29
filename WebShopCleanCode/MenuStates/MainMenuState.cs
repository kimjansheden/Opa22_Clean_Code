using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

public class MainMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _webShop;
    private IMenuState _currentState;
    private IMenuState _previousState;
    private int _currentChoice;
    private Dictionary<StatesEnum, IMenuState> _states;
    private List<IState> _stateHistory;
    private string _loginState;
    private Strings _strings;
    private Dictionary<int, Action> _optionActions;
    private List<string> _options;

    public MainMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _currentState = CurrentState;
        _previousState = PreviousState;
        _states = States;
        _stateHistory = StateHistory;
        _strings = webShopMenu.Strings;
        _currentChoice = CurrentChoice;
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
    public IMenuState CurrentState
    {
        get => _webShopMenu.CurrentState;
        set => _webShopMenu.CurrentState = value;
    }

    public IMenuState PreviousState
    {
        get => _webShopMenu.PreviousMenuState;
        set => _webShopMenu.PreviousMenuState = value;
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
        _loginState = _webShop.LoginState is LoggedInState ? _strings.LogoutString : _strings.LoginString;
        _options[_options.FindIndex(o => o == null || o == _strings.LogoutString || o == _strings.LoginString)] = _loginState;
    }

    private void LoginOrLogout()
    {
        if (_webShop.LoginState is LoggedOutState)
        {
            ChangeState(StatesEnum.LoginMenu);
        }
        else if (_webShop.LoginState is LoggedInState)
        {
            _options[2] = "Login";
            Console.WriteLine();
            Console.WriteLine(_webShop.currentCustomer.Username + " logged out.");
            Console.WriteLine();
            CurrentChoice = 1;
            _webShop.currentCustomer = null;
            _webShop.LoginState = new LoggedOutState(_webShop, _webShopMenu);
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