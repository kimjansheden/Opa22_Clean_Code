using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

internal class PurchaseMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _webShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginMessage;
    private Strings _strings;
    private int _amountOfOptions;
    public int AmountOfOptions
    {
        get => _webShopMenu.AmountOfOptions;
        set => _webShopMenu.AmountOfOptions = value;
    }
    public PurchaseMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _strings = webShopMenu.Strings;
        _webShopMenu.CurrentChoice = 1;
    }

    public void DisplayOptions()
    {
        _webShop.LoginState.RequestHandle();
        for (int i = 0; i < AmountOfOptions; i++)
        {
            Console.WriteLine(i + 1 + ": " + _webShop.products[i].Name + ", " + _webShop.products[i].Price + "kr");
        }
        Console.WriteLine("Your funds: " + _webShop.currentCustomer.Funds);
        _webShopMenu.ClearAllOptions();
        _webShopMenu.CurrentMenu = _strings.PurchaseMenu;
    }

    public void ExecuteOption(int option)
    {
        int index = _webShopMenu.CurrentChoice - 1;
        Product product = _webShop.products[index];
        if (product.InStock())
        {
            if (_webShop.currentCustomer.CanAfford(product.Price))
            {
                _webShop.currentCustomer.Funds -= product.Price;
                product.NrInStock--;
                _webShop.currentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
                Console.WriteLine();
                Console.WriteLine("Successfully bought " + product.Name);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You cannot afford.");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Not in stock.");
            Console.WriteLine();
        }
    }
}