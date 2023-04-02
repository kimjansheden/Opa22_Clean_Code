namespace WebShopCleanCode.AbstractClasses;
public abstract class State
{
    private App _app;
    private WebShop _webShop;

    protected App App
    {
        get => _app;
        init => _app = value;
    }
    protected Strings Strings => App.Strings;

    protected WebShop WebShop
    {
        get => _webShop;
        init => _webShop = value;
    }

    protected int AmountOfOptions
    {
        get => App.AmountOfOptions;
        set => App.AmountOfOptions = value;
    }

    protected Customer CurrentCustomer
    {
        get => WebShop.CurrentCustomer;
        set => WebShop.CurrentCustomer = value;
    }

    protected State CurrentState
    {
        get => App.CurrentState;
        set => App.CurrentState = value;
    }
    private State PreviousState
    {
        set => App.PreviousState = value;
    }
    private List<State> StateHistory => App.StateHistory;

    protected Dictionary<string, MenuState> States => App.MenuStates;

    protected int CurrentChoice
    {
        get => App.CurrentChoice;
        set => App.CurrentChoice = value;
    }
    protected void PrintMessageWithPadding(string message)
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Console.WriteLine();
    }
    protected virtual void ChangeState(string state)
    {
        if (CurrentState is MenuState) PreviousState = CurrentState;
        if (CurrentState is MenuState) StateHistory.Add(CurrentState);
        CurrentState = States[state];
        CurrentChoice = 1;
    }
}