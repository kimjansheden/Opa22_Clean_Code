namespace WebShopCleanCode.Interfaces;

public interface ILoginState : IState
{
    void RequestHandle();
    void LoginLogoutHandle();
    void ChangeState(StatesEnum stateEnum);
}