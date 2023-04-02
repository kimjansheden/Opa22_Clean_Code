using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode.AbstractClasses;

public abstract class MenuState : State
{
    private List<string> _options;
    private string _loginState;
    private Dictionary<int, Action> _optionActions;

    protected Dictionary<int, Action> OptionActions
    {
        get => _optionActions;
        init => _optionActions = value;
    }

    protected string LoginState
    {
        get => _loginState;
        private set => _loginState = value;
    }

    protected List<string> Options
    {
        get => _options;
        init => _options = value;
    }

    protected internal abstract void DisplayOptions();

    protected internal virtual void ExecuteOption(int option)
    {
        OptionActions[option]();
    }
    
    protected void SetLoginState()
    {
        LoginState = App.LoginState is LoggedInState ? Strings.LogoutString : Strings.LoginString;
        Options[Options.FindIndex(o => o == null || o == Strings.LogoutString || o == Strings.LoginString)] = LoginState;
    }
}