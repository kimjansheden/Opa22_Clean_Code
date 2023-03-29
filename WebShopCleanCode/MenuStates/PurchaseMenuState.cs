using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

internal class PurchaseMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _webShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginMessage;
    private int _amountOfOptions;

    private int AmountOfOptions
    {
        get => _webShopMenu.AmountOfOptions;
        set => _webShopMenu.AmountOfOptions = value;
    }

    private IMenuState CurrentState
    {
        get => _webShopMenu.CurrentState;
        set => _webShopMenu.CurrentState = value;
    }

    private IMenuState PreviousState
    {
        get => _webShopMenu.PreviousMenuState;
        set => _webShopMenu.PreviousMenuState = value;
    }

    private Dictionary<StatesEnum, IMenuState> States
    {
        get => _webShopMenu.States;
        set => _webShopMenu.States = value;
    }

    private int CurrentChoice
    {
        get => _webShopMenu.CurrentChoice;
        set => _webShopMenu.CurrentChoice = value;
    }
    private List<IState> StateHistory
    {
        get => _webShopMenu.StateHistory;
        set => _webShopMenu.StateHistory = value;
    }
    public PurchaseMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
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
    }

    public void ExecuteOption(int option)
    {
        int index = CurrentChoice - 1;
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

    public void ChangeState(StatesEnum stateEnum)
    {
        PreviousState = this;
        CurrentState = States[stateEnum];
        CurrentChoice = 1;
        StateHistory.Add(this);
    }
}