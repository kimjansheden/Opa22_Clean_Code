using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode;
public class LeftCommand : ICommand
{
    private readonly WebShopMenu _webShopMenu;
    public LeftCommand(WebShopMenu webShopMenu)
    {
        _webShopMenu = webShopMenu;
    }
    public void Execute()
    {
        if (_webShopMenu.CurrentChoice > 1)
        {
            _webShopMenu.CurrentChoice--;
        }
    }
}