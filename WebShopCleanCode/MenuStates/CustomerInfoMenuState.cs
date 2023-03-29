using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode.MenuStates;
public class CustomerInfoMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _webShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginState;
    private Strings _strings;
    private List<string> _options;
    public CustomerInfoMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _strings = webShopMenu.Strings;
        _optionActions = new Dictionary<int, Action>
        {
            { 1, SeeOrders },
            { 2, SeeInfo },
            { 3, AddFunds }
        };
        _options = new List<string>
        {
            _webShopMenu.Strings.Customer.Option1,
            _webShopMenu.Strings.Customer.Option2,
            _webShopMenu.Strings.Customer.Option3,
        };
        webShopMenu.CurrentChoice = 1;
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
    private void AddFunds()
    {
        Console.WriteLine("How many funds would you like to add?");
        string amountString = Console.ReadLine();
        try
        {
            int amount = int.Parse(amountString);
            if (amount < 0)
            {
                Console.WriteLine();
                Console.WriteLine("Don't add negative amounts.");
                Console.WriteLine();
            }
            else
            {
                _webShop.currentCustomer.Funds += amount;
                Console.WriteLine();
                Console.WriteLine(amount + " added to your profile.");
                Console.WriteLine();
            }
        }
        catch (FormatException e)
        {
            Console.WriteLine();
            Console.WriteLine("Please write a number next time.");
            Console.WriteLine();
        }
    }

    private void SeeInfo()
    {
        _webShop.currentCustomer.PrintInfo();
    }

    private void SeeOrders()
    {
        _webShop.currentCustomer.PrintOrders();
    }

    public void DisplayOptions()
    {
        if (_webShop.LoginState is LoggedOutState)
        {
            Console.WriteLine();
            Console.WriteLine("Nobody is logged in.");
            Console.WriteLine();
        }
        else if (_webShop.LoginState is LoggedInState)
        {
            _webShopMenu.SetOptions(_options);
            _webShopMenu.AmountOfOptions = 3;
            Console.WriteLine(_strings.MenuWhat);
            _webShopMenu.PrintOptions();
        }
    }

    public void ExecuteOption(int option)
    {
        _optionActions[option]();
    }

    public void ChangeState(StatesEnum stateEnum)
    {
        PreviousState = this;
        CurrentState = States[stateEnum];
        CurrentChoice = 1;
        StateHistory.Add(this);
    }
}