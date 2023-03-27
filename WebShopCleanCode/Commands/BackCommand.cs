using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode;
public class BackCommand : ICommand
{
    private readonly WebShopMenu _webShopMenu;
    public BackCommand(WebShopMenu webShopMenu)
    {
        _webShopMenu = webShopMenu;
    }
    public void Execute()
    {
        _webShopMenu.CurrentMenu = _webShopMenu.PreviousMenu;
    }
}