using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class BackCommand : ICommand
{
    private IApp _app;
    public ICommand Initialize(IApp app)
    {
        _app = app;
        return this;
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