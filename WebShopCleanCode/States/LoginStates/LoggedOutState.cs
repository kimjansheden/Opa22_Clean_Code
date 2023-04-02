using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.LoginStates;

public class LoggedOutState : LoginState
{
    public LoggedOutState(App app)
    {
        App = app;
    }

    protected internal override void RequestHandle()
    {
        PrintMessageWithPadding(((DefaultStrings)Strings).Login.MustBeLoggedIn);
        App.Commands["back"].Execute();
        App.DisplayOptions();
    }

    protected internal override void LoginLogoutHandle()
    {
        ChangeState("LoginMenu");
    }
}