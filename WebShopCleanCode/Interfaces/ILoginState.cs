namespace WebShopCleanCode.Interfaces;

public interface ILoginState : IState
{
    void RequestHandle();
    void LoginOutHandle();
    void ChangeState(StatesEnum stateEnum);
}