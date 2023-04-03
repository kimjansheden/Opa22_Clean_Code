using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode.AbstractClasses;

public abstract class MenuState : State
{
    private string _loginStateStringString;
    private Dictionary<int, Action> _optionActions;

    protected virtual Dictionary<int, Action> OptionActions
    {
        get => _optionActions;
        set => _optionActions = value;
    }

    protected virtual string LoginStateString
    {
        get => _loginStateStringString;
        private set => _loginStateStringString = value;
    }

    public MenuState(App app, WebShop webShop) : base(app, webShop)
    {
        
    }

    protected internal abstract void DisplayOptions();
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