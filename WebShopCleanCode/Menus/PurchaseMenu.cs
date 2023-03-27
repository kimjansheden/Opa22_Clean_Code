using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class PurchaseMenu : IMenu
{
    private int _amountOfOptions;
    
    private readonly WebShop _webShop;
    private readonly WebShopMenu _webShopMenu;
    private Strings _strings;

    public int AmountOfOptions
    {
        get => _webShopMenu.AmountOfOptions;
        set => _webShopMenu.AmountOfOptions = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="webShop"></param>
    /// <param name="webShopMenu"></param>
    public PurchaseMenu(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;
    }

    public void Run()
    {
        for (int i = 0; i < AmountOfOptions; i++)
        {
            Console.WriteLine(i + 1 + ": " + _webShop.products[i].Name + ", " + _webShop.products[i].Price + "kr");
        }
        Console.WriteLine("Your funds: " + _webShop.currentCustomer.Funds);
        _webShopMenu.ClearAllOptions();
        _webShopMenu.CurrentMenu = _strings.PurchaseMenu;
    }
}