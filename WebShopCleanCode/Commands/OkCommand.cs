using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;

public class OkCommand : ICommand
{
    private readonly WebShopMenu _webShopMenu;
    public OkCommand(WebShopMenu webShopMenu)
    {
        _webShopMenu = webShopMenu;
    }
    public void Execute()
    {
        ((MenuState)_webShopMenu.CurrentState).ExecuteOption(_webShopMenu.CurrentChoice);
    }
}