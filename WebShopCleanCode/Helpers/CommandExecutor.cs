using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Helpers;

public class CommandExecutor : ICommandExecutor
{
    private IApp _app;

    public CommandExecutor(IApp app)
    {
        _app = app;
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