using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class BackCommand : ICommand
{
    private readonly App _app;
    public BackCommand(App app)
    {
        _app = app;
    }

    private State LastState => (_app.StateHistory.Count != 0 ? _app.StateHistory[^1] : default)!;

    public void Execute()
    {
        if (LastState is MenuState menuState)
        {
            _app.CurrentState = menuState;
            _app.StateHistory.Remove(LastState);
            _app.CurrentChoice = 1;
        }
    }
}