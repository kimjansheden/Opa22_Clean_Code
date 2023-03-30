using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

public class PurchaseMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _defaultWebShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginMessage;
    private int _amountOfOptions;

    private IState CurrentState
    {
        get => _webShopMenu.CurrentState;
        set => _webShopMenu.CurrentState = value;
    }

    private IState PreviousState
    {
        get => _webShopMenu.PreviousState;
        set => _webShopMenu.PreviousState = value;
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
    public PurchaseMenuState(WebShopMenu webShopMenu, WebShop defaultWebShop)
    {
        _webShopMenu = webShopMenu;
        _defaultWebShop = defaultWebShop;
    }

    public void DisplayOptions()
    {
        ((ILoginState)_webShopMenu.LoginState).RequestHandle();
    }

    public void ExecuteOption(int option)
    {
        int index = CurrentChoice - 1;
        Product product = _defaultWebShop.Products[index];
        if (product.InStock())
        {
            if (_defaultWebShop.CurrentCustomer.CanAfford(product.Price))
            {
                _defaultWebShop.CurrentCustomer.Funds -= product.Price;
                product.NrInStock--;
                _defaultWebShop.CurrentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
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