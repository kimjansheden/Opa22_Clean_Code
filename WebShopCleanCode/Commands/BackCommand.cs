using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Commands;
public class BackCommand : ICommand
{
    private readonly WebShopMenu _webShopMenu;
    public BackCommand(WebShopMenu webShopMenu)
    {
        _webShopMenu = webShopMenu;
    }

    private IState LastState => (_webShopMenu.StateHistory.Count != 0 ? _webShopMenu.StateHistory[^1] : default)!;

    public void Execute()
    {
        
        if (LastState is IMenuState menuState)
        {
            _webShopMenu.CurrentState = menuState;
            _webShopMenu.StateHistory.Remove(LastState);
            _webShopMenu.CurrentChoice = 1;
        }
    }
}