using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;

public class OkCommand : ICommand
{
    private IApp _app;
    public ICommand Initialize(IApp app)
    {
        _app = app;
        return this;
    }
    public void Execute()
    {
        ((MenuState)_app.CurrentState).ExecuteOption(_app.CurrentChoice);
    }
}