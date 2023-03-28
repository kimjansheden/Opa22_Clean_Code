using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode;

public class OkCommand : ICommand
{
    private readonly WebShop _webShop;
    private readonly WebShopMenu _webShopMenu;
    private Strings _strings;
    private Dictionary<int, IMenu> _menuMap;
    public OkCommand(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;

        var choice = 1;
        _menuMap = new Dictionary<int, IMenu>();
        foreach (var menu in _webShopMenu.Menus)
        {
            _menuMap.Add(choice++, menu.Value);
        }
    }

    public void Execute()
    {
        _webShopMenu.CurrentState.ExecuteOption(_webShopMenu.CurrentChoice);
    }
}