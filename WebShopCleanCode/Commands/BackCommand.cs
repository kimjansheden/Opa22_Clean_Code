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
        //_webShopMenu.CurrentMenu = _webShopMenu.PreviousMenu;
        // Back måste vara en hierarki ju där den går upp 1 hela tiden, inte bara går bakåt
        _webShopMenu.CurrentState = _webShopMenu.PreviousMenuState;
        _webShopMenu.CurrentChoice = 1;
    }
}