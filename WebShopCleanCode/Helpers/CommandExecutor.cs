using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Helpers;

public class CommandExecutor : ICommandExecutor
{
    private IApp _app;

    public ICommandExecutor Initialize(IApp app)
    {
        _app = app;
        return this;
    }

    public void ExecuteCommandIfExists(string input)
    {
        if (_app.Commands.ContainsKey(input))
        {
            _app.Commands[input].Execute();
            _app.CurrentCommand = _app.Commands[input];
        }
        else
        {
            Console.WriteLine(_app.Strings.NotApplicable);
        }
    }
}