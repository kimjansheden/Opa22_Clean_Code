using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class QuitCommand : ICommand
{
    private IApp _app;

    public ICommand Initialize(IApp app)
    {
        _app = app;
        return this;
    }
    public void Execute()
    {
        Console.WriteLine(_app.Strings.PowerDown);
    }
}