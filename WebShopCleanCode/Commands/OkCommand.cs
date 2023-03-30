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
        if (_webShopMenu.CurrentState is IMenuState menuState)
        {
            menuState.ExecuteOption(_webShopMenu.CurrentChoice);
        }
    }
}