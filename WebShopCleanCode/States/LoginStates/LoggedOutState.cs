using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.States.MenuStates;

namespace WebShopCleanCode.States.LoginStates;

public class LoggedOutState : LoginState
{
    public LoggedOutState(WebShop webShop, App app) : base(app, webShop)
    {
        
    }

    protected internal override void RequestHandle(State state)
    {
        if (state is PurchaseMenuState)
        {
            PurchaseMenuHandle();    
        }
        else if (state is CustomerInfoMenuState)
        {
            CustomerInfoMenuHandle();
        }
    }

    protected internal override void LoginLogoutHandle()
    {
        ChangeState("LoginMenu");
    }
    
    private void PurchaseMenuHandle()
    {
        PrintMessageWithPadding(((DefaultStrings)Strings).Login.MustBeLoggedIn);
        App.Commands["back"].Execute();
        App.DisplayOptions();
    }

    private void CustomerInfoMenuHandle()
    {
        PrintMessageWithPadding(((DefaultStrings)Strings).Login.NobodyLoggedIn);
        App.Commands["back"].Execute();
        App.DisplayOptions();
    }
}