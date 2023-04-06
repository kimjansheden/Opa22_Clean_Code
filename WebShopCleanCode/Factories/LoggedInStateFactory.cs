using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode.Factories;

public class LoggedInStateFactory : ILoginStateFactory
{
    public LoginState CreateState(App app, WebShop webShop)
    {
        var state = new LoggedInState(webShop, app);
        return state;
    }
}