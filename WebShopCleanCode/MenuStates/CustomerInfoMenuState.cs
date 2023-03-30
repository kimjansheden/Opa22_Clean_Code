using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.LoginStates;

namespace WebShopCleanCode.MenuStates;
public class CustomerInfoMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _defaultWebShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginState;
    private Strings _strings;
    private List<string> _options;
    public CustomerInfoMenuState(WebShopMenu webShopMenu, WebShop defaultWebShop)
    {
        _webShopMenu = webShopMenu;
        _defaultWebShop = defaultWebShop;
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
                _defaultWebShop.CurrentCustomer.Funds += amount;
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
        _defaultWebShop.CurrentCustomer.PrintInfo();
    }

    private void SeeOrders()
    {
        _defaultWebShop.CurrentCustomer.PrintOrders();
    }

    public void DisplayOptions()
    {
        if (_webShopMenu.LoginState is LoggedOutState)
        {
            Console.WriteLine();
            Console.WriteLine("Nobody is logged in.");
            Console.WriteLine();
        }
        else if (_webShopMenu.LoginState is LoggedInState)
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