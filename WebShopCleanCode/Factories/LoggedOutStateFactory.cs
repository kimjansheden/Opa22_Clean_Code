using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode.Factories;

public class LoggedOutStateFactory : ILoginStateFactory
{
    public LoginState CreateState(App app, WebShop webShop)
    {
        var state = new LoggedOutState(webShop, app);
        return state;
    }
}