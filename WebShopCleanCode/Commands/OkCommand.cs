using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;

public class OkCommand : ICommand
{
    private readonly App _app;
    public OkCommand(App app)
    {
        _app = app;
    }
    public void Execute()
    {
        ((MenuState)_app.CurrentState).ExecuteOption(_app.CurrentChoice);
    }
}