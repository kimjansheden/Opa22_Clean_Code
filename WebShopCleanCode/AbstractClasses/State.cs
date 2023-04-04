namespace WebShopCleanCode.AbstractClasses;

// I decided to go with the State Design Pattern in order to give the menus a dynamic behavior that also answers to whether the customer is logged in or not. The State Pattern together with the Command Pattern helped me get rid of the switch cases and if statements that the old code used to navigate in the menu and perform actions.
public abstract class State
{
    private readonly App _app;
    private readonly WebShop _webShop;
    private List<string> _options;

    protected App App => _app;
    protected virtual Strings Strings => App.Strings;

    protected WebShop WebShop => _webShop;

    protected virtual int AmountOfOptions
    {
        get => App.AmountOfOptions;
        set => App.AmountOfOptions = value;
    }
    protected internal virtual List<string> Options
    {
        get => _options;
        set => _options = value;
    }

    protected virtual Customer CurrentCustomer
    {
        get => WebShop.CurrentCustomer;
        set => WebShop.CurrentCustomer = value;
    }

    protected virtual State CurrentState
    {
        get => App.CurrentState;
        set => App.CurrentState = value;
    }

    protected virtual LoginState LoginState
    {
        get => App.LoginState;
        set => App.LoginState = value;
    }
    
    private protected virtual List<State> StateHistory => App.StateHistory;

    protected virtual Dictionary<string, MenuState> States => App.MenuStates;

    protected virtual int CurrentChoice
    {
        get => App.CurrentChoice;
        set => App.CurrentChoice = value;
    }

    protected State(App app, WebShop webShop)
    {
        _app = app;
        _webShop = webShop;
    }

    protected State(App app)
    {
        _app = app;
    }

    protected virtual void PrintMessageWithPadding(string message)
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Console.WriteLine();
    }
    protected virtual void ChangeState(string state)
    {
        if (CurrentState is MenuState) StateHistory.Add(CurrentState);
        CurrentState = States[state];
        CurrentChoice = 1;
    }
}