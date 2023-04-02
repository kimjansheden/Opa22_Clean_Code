using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class RightCommand : ICommand
{
    private readonly App _app;
    public RightCommand(App app)
    {
        _app = app;
    }
    public void Execute()
    {
        if (_app.CurrentChoice < _app.AmountOfOptions)
        {
            _app.CurrentChoice++;
        }
    }
}