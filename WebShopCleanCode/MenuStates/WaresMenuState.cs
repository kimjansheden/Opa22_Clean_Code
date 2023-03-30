using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.LoginStates;

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
        CurrentChoice = 1;
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

    private List<IState> StateHistory
    {
        get => _webShopMenu.StateHistory;
        set => _webShopMenu.StateHistory = value;
    }

    private void LoginOrLogout()
    {
        ((ILoginState)_webShopMenu.LoginState).LoginOutHandle();
    }

    // Den här skulle kunna vara gemensam. Kanske ändra om IMenuState till abstract så alla kan dela på den?
    private void SetLoginState()
    {
        _loginState = _webShopMenu.LoginState is LoggedInState ? _strings.LogoutString : _strings.LoginString;
        _options[_options.FindIndex(o => o == null || o == _strings.LogoutString || o == _strings.LoginString)] = _loginState;
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

    public void DisplayOptions()
    {
        SetLoginState();
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 4;
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
        StateHistory.Add(this);
        CurrentState = States[stateEnum];
        CurrentChoice = 1;
    }
}