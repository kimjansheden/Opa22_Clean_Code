using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.LoginStates;

namespace WebShopCleanCode.MenuStates;

public class LoginMenuState : MenuState
{
    public LoginMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
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
    private void Register()
    {
        Console.WriteLine(_strings.Login.WriteUsername);
        string newUsername = Console.ReadLine();
        foreach (Customer customer in _webShop.Customers)
        {
            if (customer.Username.Equals(_webShopMenu.Username))
            {
                PrintMessageWithPadding(_strings.Login.UsernameExists);
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
                        PrintMessageWithPadding(_strings.Login.WriteSomething);
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
            PrintMessageWithPadding(_strings.Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(_strings.Login.WantFirstName);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(_strings.Login.WriteFirstName);
                    firstName = Console.ReadLine();
                    if (firstName.Equals(""))
                    {
                        PrintMessageWithPadding(_strings.Login.WriteSomething);
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
            PrintMessageWithPadding(_strings.Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(_strings.Login.WantLastName);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(_strings.Login.WriteLastName);
                    lastName = Console.ReadLine();
                    if (lastName.Equals(""))
                    {
                        PrintMessageWithPadding(_strings.Login.WriteSomething);
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
            PrintMessageWithPadding(_strings.Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(_strings.Login.WantEmail);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(_strings.Login.WriteEmail);
                    email = Console.ReadLine();
                    if (email.Equals(""))
                    {
                        PrintMessageWithPadding(_strings.Login.WriteSomething);
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
            PrintMessageWithPadding(_strings.Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(_strings.Login.WantAge);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(_strings.Login.WriteAge);
                    string ageString = Console.ReadLine();
                    try
                    {
                        age = int.Parse(ageString);
                    }
                    catch (FormatException e)
                    {
                        PrintMessageWithPadding(_strings.Login.WriteNumber);
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
            PrintMessageWithPadding(_strings.Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(_strings.Login.WantAddress);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(_strings.Login.WriteAddress);
                    address = Console.ReadLine();
                    if (address.Equals(""))
                    {
                        PrintMessageWithPadding(_strings.Login.WriteSomething);
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
            PrintMessageWithPadding(_strings.Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(_strings.Login.WantPhone);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(_strings.Login.WritePhone);
                    phoneNumber = Console.ReadLine();
                    if (phoneNumber.Equals(""))
                    {
                        PrintMessageWithPadding(_strings.Login.WriteSomething);
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
            PrintMessageWithPadding(_strings.Login.YOrN);
        }
    
        Customer newCustomer = new Customer(newUsername, newPassword, firstName, lastName, email, age, address, phoneNumber);
        _webShop.Customers.Add(newCustomer);
        _webShop.CurrentCustomer = newCustomer;
        _webShopMenu.LoginState = new LoggedInState(_webShop, _webShopMenu);
        PrintMessageWithPadding(newCustomer.Username + " successfully added and is now logged in.");
        ChangeState(StatesEnum.MainMenu);
    }

    private void Login()
    {
        if (_webShopMenu.Username == null || _webShopMenu.Password == null)
        {
            PrintMessageWithPadding(_strings.Login.IncompleteData);
        }
        else
        {
            bool found = false;
            foreach (Customer customer in _webShop.Customers)
            {
                if (_webShopMenu.Username.Equals(customer.Username) && customer.CheckPassword(_webShopMenu.Password))
                {
                    PrintMessageWithPadding(customer.Username + " logged in.");
                    _webShop.CurrentCustomer = customer;
                    found = true;
                    ChangeState(StatesEnum.MainMenu);
                    break;
                }
            }
            if (found == false)
            {
                PrintMessageWithPadding(_strings.Login.InvalidCreds);
            }
        }
    }

    private void SetPassword()
    {
        Console.WriteLine(_strings.Login.AKeyBoard);
        Console.WriteLine(_strings.Login.InputPassword);
        _webShopMenu.Password = Console.ReadLine();
        Console.WriteLine();
    }

    private void SetUsername()
    {
        Console.WriteLine(_strings.Login.AKeyBoard);
        Console.WriteLine(_strings.Login.InputUsername);
        _webShopMenu.Username = Console.ReadLine();
        Console.WriteLine();
    }

    protected internal override void DisplayOptions()
    {
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 4;
        Console.WriteLine(_strings.Login.Menu);
        _webShopMenu.PrintOptions();
    }
}