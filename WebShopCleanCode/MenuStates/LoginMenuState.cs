using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.LoginStates;

namespace WebShopCleanCode.MenuStates;

public class LoginMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _defaultWebShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginState;
    private Strings _strings;
    private List<string> _options;

    public LoginMenuState(WebShopMenu webShopMenu, WebShop defaultWebShop)
    {
        _webShopMenu = webShopMenu;
        _defaultWebShop = defaultWebShop;
        _strings = webShopMenu.Strings;
        _optionActions = new Dictionary<int, Action>
        {
            { 1, SetUsername },
            { 2, SetPassword },
            { 3, Login },
            { 4, Register }
        };
        _options = new List<string>
        {
            _webShopMenu.Strings.Login.Option1,
            _webShopMenu.Strings.Login.Option2,
            _webShopMenu.Strings.Login.Option3,
            _webShopMenu.Strings.Login.Option4
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

    private void Register()
    {
        Console.WriteLine(_strings.Login.WriteUsername);
        string newUsername = Console.ReadLine();
        foreach (Customer customer in _defaultWebShop.Customers)
        {
            if (customer.Username.Equals(_webShopMenu.Username))
            {
                Console.WriteLine();
                Console.WriteLine(_strings.Login.UsernameExists);
                Console.WriteLine();
                break;
            }
        }
        string choice = "";
        bool next = false;
        string newPassword = null;
        string firstName = null;
        string lastName = null;
        string email = null;
        int age = -1;
        string address = null;
        string phoneNumber = null;
        while (true)
        {
            Console.WriteLine("Do you want a password? y/n");
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine("Please write your password.");
                    newPassword = Console.ReadLine();
                    if (newPassword.Equals(""))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please actually write something.");
                        Console.WriteLine();
                    }
                    else
                    {
                        next = true;
                        break;
                    }
                }
            }
            if (choice.Equals("n") || next)
            {
                next = false;
                break;
            }
            Console.WriteLine();
            Console.WriteLine("y or n, please.");
            Console.WriteLine();
        }
        while (true)
        {
            Console.WriteLine("Do you want a first name? y/n");
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine("Please write your first name.");
                    firstName = Console.ReadLine();
                    if (firstName.Equals(""))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please actually write something.");
                        Console.WriteLine();
                    }
                    else
                    {
                        next = true;
                        break;
                    }
                }
            }
            if (choice.Equals("n") || next)
            {
                next = false;
                break;
            }
            Console.WriteLine();
            Console.WriteLine("y or n, please.");
            Console.WriteLine();
        }
        while (true)
        {
            Console.WriteLine("Do you want a last name? y/n");
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine("Please write your last name.");
                    lastName = Console.ReadLine();
                    if (lastName.Equals(""))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please actually write something.");
                        Console.WriteLine();
                    }
                    else
                    {
                        next = true;
                        break;
                    }
                }
            }
            if (choice.Equals("n") || next)
            {
                next = false;
                break;
            }
            Console.WriteLine();
            Console.WriteLine("y or n, please.");
            Console.WriteLine();
        }
        while (true)
        {
            Console.WriteLine("Do you want an email? y/n");
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine("Please write your email.");
                    email = Console.ReadLine();
                    if (email.Equals(""))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please actually write something.");
                        Console.WriteLine();
                    }
                    else
                    {
                        next = true;
                        break;
                    }
                }
            }
            if (choice.Equals("n") || next)
            {
                next = false;
                break;
            }
            Console.WriteLine();
            Console.WriteLine("y or n, please.");
            Console.WriteLine();
        }
        while (true)
        {
            Console.WriteLine("Do you want an age? y/n");
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine("Please write your age.");
                    string ageString = Console.ReadLine();
                    try
                    {
                        age = int.Parse(ageString);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please write a number.");
                        Console.WriteLine();
                        continue;
                    }
                    next = true;
                    break;
                }
            }
            if (choice.Equals("n") || next)
            {
                next = false;
                break;
            }
            Console.WriteLine();
            Console.WriteLine("y or n, please.");
            Console.WriteLine();
        }
        while (true)
        {
            Console.WriteLine("Do you want an address? y/n");
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine("Please write your address.");
                    address = Console.ReadLine();
                    if (address.Equals(""))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please actually write something.");
                        Console.WriteLine();
                    }
                    else
                    {
                        next = true;
                        break;
                    }
                }
            }
            if (choice.Equals("n") || next)
            {
                next = false;
                break;
            }
            Console.WriteLine();
            Console.WriteLine("y or n, please.");
            Console.WriteLine();
        }
        while (true)
        {
            Console.WriteLine("Do you want a phone number? y/n");
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine("Please write your phone number.");
                    phoneNumber = Console.ReadLine();
                    if (phoneNumber.Equals(""))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please actually write something.");
                        Console.WriteLine();
                    }
                    else
                    {
                        next = true;
                        break;
                    }
                }
            }
            if (choice.Equals("n") || next)
            {
                break;
            }
            Console.WriteLine();
            Console.WriteLine("y or n, please.");
            Console.WriteLine();
        }
    
        Customer newCustomer = new Customer(newUsername, newPassword, firstName, lastName, email, age, address, phoneNumber);
        _defaultWebShop.Customers.Add(newCustomer);
        _defaultWebShop.CurrentCustomer = newCustomer;
        _webShopMenu.LoginState = new LoggedInState(_defaultWebShop, _webShopMenu);
        Console.WriteLine();
        Console.WriteLine(newCustomer.Username + " successfully added and is now logged in.");
        Console.WriteLine();
        ChangeState(StatesEnum.MainMenu);
    }

    private void Login()
    {
        if (_webShopMenu.Username == null || _webShopMenu.Password == null)
        {
            Console.WriteLine();
            Console.WriteLine("Incomplete data.");
            Console.WriteLine();
        }
        else
        {
            bool found = false;
            foreach (Customer customer in _defaultWebShop.Customers)
            {
                if (_webShopMenu.Username.Equals(customer.Username) && customer.CheckPassword(_webShopMenu.Password))
                {
                    Console.WriteLine();
                    Console.WriteLine(customer.Username + " logged in.");
                    Console.WriteLine();
                    _defaultWebShop.CurrentCustomer = customer;
                    found = true;
                    ChangeState(StatesEnum.MainMenu);
                    break;
                }
            }
            if (found == false)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid credentials.");
                Console.WriteLine();
            }
        }
    }

    private void SetPassword()
    {
        Console.WriteLine(_strings.Login.AKeyBoard);
        Console.WriteLine("Please input your password.");
        _webShopMenu.Password = Console.ReadLine();
        Console.WriteLine();
    }

    private void SetUsername()
    {
        Console.WriteLine(_strings.Login.AKeyBoard);
        Console.WriteLine("Please input your username.");
        _webShopMenu.Username = Console.ReadLine();
        Console.WriteLine();
    }

    public void DisplayOptions()
    {
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 4;
        Console.WriteLine(_strings.Login.Menu);
        _webShopMenu.PrintOptions();
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