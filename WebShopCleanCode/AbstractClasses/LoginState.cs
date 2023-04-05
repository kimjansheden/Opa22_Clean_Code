using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.AbstractClasses;

public abstract class LoginState : State
{
    protected internal abstract void RequestHandle(State state);
    protected internal abstract void LoginLogoutHandle();

    protected LoginState(IApp app, WebShop webShop) : base(app, webShop)
    {
    }

    protected LoginState(IApp app) : base(app)
    {
    }
}