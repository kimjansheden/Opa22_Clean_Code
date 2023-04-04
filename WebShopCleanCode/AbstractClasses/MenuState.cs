using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode.AbstractClasses;

public abstract class MenuState : State
{
    private string _loginStateString;
    private string _displayMessage;
    private Dictionary<int, Action> _optionActions;

    protected virtual Dictionary<int, Action> OptionActions
    {
        get => _optionActions;
        set => _optionActions = value;
    }

    protected virtual string LoginStateString
    {
        get => _loginStateString;
        private set => _loginStateString = value;
    }

    protected virtual string DisplayMessage => _displayMessage;

    public MenuState(App app, WebShop webShop) : base(app, webShop)
    {
        
    }

    protected internal virtual void DisplayOptions()
    {
        App.SetOptions(Options);
        AmountOfOptions = Options.Count;
        Console.WriteLine(DisplayMessage);
        App.PrintOptions();
    }
    protected internal abstract void Initialize();

    protected internal virtual void ExecuteOption(int option)
    {
        OptionActions[option]();
    }
    
    protected virtual void SetLoginState()
    {
        LoginStateString = App.LoginState is LoggedInState ? Strings.LogoutString : Strings.LoginString;
        Options[Options.FindIndex(o => o == null || o == Strings.LogoutString || o == Strings.LoginString)] = LoginStateString;
    }
}