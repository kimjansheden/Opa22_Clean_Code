using WebShopCleanCode.Interfaces;
using WebShopCleanCode.LoginStates;

namespace WebShopCleanCode.AbstractClasses;

public abstract class MenuState : IState
{
    protected WebShopMenu _webShopMenu { get; set; }
    protected WebShop _webShop { get; set; }
    protected Strings _strings { get; set; }
    protected Dictionary<int, Action> _optionActions;
    protected string _loginState;
    protected List<string> _options;

    protected IState CurrentState
    {
        get => _webShopMenu.CurrentState;
        set => _webShopMenu.CurrentState = value;
    }

    protected IState PreviousState
    {
        get => _webShopMenu.PreviousState;
        set => _webShopMenu.PreviousState = value;
    }

    protected Dictionary<StatesEnum, MenuState> States
    {
        get => _webShopMenu.States;
        set => _webShopMenu.States = value;
    }

    protected int CurrentChoice
    {
        get => _webShopMenu.CurrentChoice;
        set => _webShopMenu.CurrentChoice = value;
    }

    protected List<IState> StateHistory
    {
        get => _webShopMenu.StateHistory;
        set => _webShopMenu.StateHistory = value;
    }

    protected internal abstract void DisplayOptions();

    protected internal virtual void ExecuteOption(int option)
    {
        _optionActions[option]();
    }

    protected void ChangeState(StatesEnum stateEnum)
    {
        PreviousState = this;
        CurrentState = States[stateEnum];
        CurrentChoice = 1;
        StateHistory.Add(this);
    }

    protected void PrintMessageWithPadding(string message)
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Console.WriteLine();
    }

    protected void SetLoginState()
    {
        _loginState = _webShopMenu.LoginState is LoggedInState ? _strings.LogoutString : _strings.LoginString;
        _options[_options.FindIndex(o => o == null || o == _strings.LogoutString || o == _strings.LoginString)] = _loginState;
    }
}