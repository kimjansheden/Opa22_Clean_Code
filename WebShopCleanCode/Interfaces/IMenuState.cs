namespace WebShopCleanCode.Interfaces;

public interface IMenuState : IState
{
    void DisplayOptions();
    void ExecuteOption(int option);
    void ChangeState(StatesEnum stateEnum);
}