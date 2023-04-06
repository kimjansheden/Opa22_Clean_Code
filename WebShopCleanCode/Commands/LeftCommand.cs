using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class LeftCommand : ICommand
{
    private IApp _app;

    public ICommand Initialize(IApp app)
    {
        _app = app;
        return this;
    }
    
    public void Execute()
    {
        if (_app.CurrentChoice > 1)
        {
            _app.CurrentChoice--;
        }
    }
}