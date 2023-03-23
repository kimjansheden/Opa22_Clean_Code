using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode;
public class RightCommand : ICommand
{
    private readonly WebShopMenu _webShopMenu;
    public RightCommand(WebShopMenu webShopMenu)
    {
        _webShopMenu = webShopMenu;
    }
    public void Execute()
    {
        if (_webShopMenu.CurrentChoice < _webShopMenu.AmountOfOptions)
        {
            _webShopMenu.CurrentChoice++;
        }
    }
}