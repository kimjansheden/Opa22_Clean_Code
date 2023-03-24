using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode;
public class QuitCommand : ICommand
{
    private readonly WebShopMenu _webShopMenu;
    public QuitCommand(WebShopMenu webShopMenu)
    {
        _webShopMenu = webShopMenu;
    }
    public void Execute()
    {
        Console.WriteLine("The console powers down. You are free to leave.");
    }
}