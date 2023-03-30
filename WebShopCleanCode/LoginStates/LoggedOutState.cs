using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.LoginStates;

public class LoggedOutState : ILoginState
{
    private readonly Strings _strings;
    private readonly WebShopMenu _webShopMenu;
    private IState CurrentState
    {
        get => _webShopMenu.CurrentState;
        set => _webShopMenu.CurrentState = value;
    }
    private Dictionary<StatesEnum, MenuState> States
    {
        get => _webShopMenu.States;
        set => _webShopMenu.States = value;
    }
    private int CurrentChoice
    {
        get => _webShopMenu.CurrentChoice;
        set => _webShopMenu.CurrentChoice = value;
    }
    public LoggedOutState(WebShopMenu webShopMenu)
    {
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;
    }
    public void RequestHandle()
    {
        Console.WriteLine();
        Console.WriteLine(_strings.Login.MustBeLoggedIn);
        Console.WriteLine();
        _webShopMenu.Commands["back"].Execute();
        _webShopMenu.DisplayOptions();
    }

    public void LoginLogoutHandle()
    {
        ChangeState(StatesEnum.LoginMenu);
    }

    public void ChangeState(StatesEnum stateEnum)
    {
        CurrentState = States[stateEnum];
        CurrentChoice = 1;
    }
}