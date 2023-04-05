using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.Interfaces;

public interface IApp
{
    string Password { get; set; }
    string Username { get; set; }
    int CurrentChoice { get; set; }
    Strings Strings { get; set; }
    int AmountOfOptions { get; set; }
    State CurrentState { get; set; }
    ICommand CurrentCommand { get; set; }
    Customer CurrentCustomer { get; set; }
    Dictionary<string, LoginState> LoginStates { get; set; }
    Dictionary<string, MenuState> MenuStates { get; set; }
    Dictionary<string, ICommand> Commands { get; set; }
    LoginState LoginState { get; set; }
    List<State> StateHistory { get; set; }
    List<string> Options { get; set; }
    void Run();
    void SetOptions(List<string> newOptions);
    void PrintOptions();
    void ClearAllOptions();
    void DisplayOptions();
}