using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class RightCommand : ICommand
{
    private IApp _app;
    public ICommand Initialize(IApp app)
    {
        _app = app;
        return this;
    }
    public void Execute()
    {
        if (_app.CurrentChoice < _app.AmountOfOptions)
        {
            _app.CurrentChoice++;
        }
    }
}