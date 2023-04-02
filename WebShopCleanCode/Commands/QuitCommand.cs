using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class QuitCommand : ICommand
{
    private readonly App _app;
    public QuitCommand(App app)
    {
        _app = app;
    }
    public void Execute()
    {
        Console.WriteLine("The console powers down. You are free to leave.");
    }
}