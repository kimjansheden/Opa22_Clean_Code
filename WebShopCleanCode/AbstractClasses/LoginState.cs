namespace WebShopCleanCode.AbstractClasses;

public abstract class LoginState : State
{
    protected internal abstract void RequestHandle();
    protected internal abstract void LoginLogoutHandle();
}