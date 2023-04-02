using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class LeftCommand : ICommand
{
    private readonly App _app;
    public LeftCommand(App app)
    {
        _app = app;
    }
    public void Execute()
    {
        if (_app.CurrentChoice > 1)
        {
            _app.CurrentChoice--;
        }
    }
}