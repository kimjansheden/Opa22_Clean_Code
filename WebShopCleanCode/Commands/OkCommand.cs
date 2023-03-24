using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode;

public class OkCommand : ICommand
{
    private readonly WebShop _webShop;
    private readonly WebShopMenu _webShopMenu;
    private Strings _strings;
    public OkCommand(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;
    }
    public void Execute()
    {
        if (_strings.currentMenu.Equals("main menu"))
        {
            switch (_webShopMenu.CurrentChoice)
            {
                case 1:
                    _webShopMenu.Options[0] = "See all wares";
                    _webShopMenu.Options[1] = "Purchase a ware";
                    _webShopMenu.Options[2] = "Sort wares";
                    if (_webShop.currentCustomer == null)
                    {
                        _webShopMenu.Options[3] = "Login";
                    }
                    else
                    {
                        _webShopMenu.Options[3] = "Logout";
                    }
                    _webShopMenu.AmountOfOptions = 4;
                    _webShopMenu.CurrentChoice = 1;
                    _strings.currentMenu = "wares menu";
                    _strings.MainMenuWhat = "What would you like to do?";
                    break;
                case 2:
                    if (_webShop.currentCustomer != null)
                    {
                        _webShopMenu.Options[0] = "See your orders";
                        _webShopMenu.Options[1] = "Set your info";
                        _webShopMenu.Options[2] = "Add funds";
                        _webShopMenu.Options[3] = "";
                        _webShopMenu.AmountOfOptions = 3;
                        _webShopMenu.CurrentChoice = 1;
                        _strings.MainMenuWhat = "What would you like to do?";
                        _strings.currentMenu = "customer menu";
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Nobody is logged in.");
                        Console.WriteLine();
                    }
                    break;
                case 3:
                    if (_webShop.currentCustomer == null)
                    {
                        _webShopMenu.Options[0] = "Set Username";
                        _webShopMenu.Options[1] = "Set Password";
                        _webShopMenu.Options[2] = "Login";
                        _webShopMenu.Options[3] = "Register";
                        _webShopMenu.AmountOfOptions = 4;
                        _webShopMenu.CurrentChoice = 1;
                        _strings.MainMenuWhat = "Please submit username and password.";
                        _webShopMenu.Username = null;
                        _webShopMenu.Password = null;
                        _strings.currentMenu = "login menu";
                    }
                    else
                    {
                        _webShopMenu.Options[2] = "Login";
                        Console.WriteLine();
                        Console.WriteLine(_webShop.currentCustomer.Username + " logged out.");
                        Console.WriteLine();
                        _webShopMenu.CurrentChoice = 1;
                        _webShop.currentCustomer = null;
                    }
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Not an option.");
                    Console.WriteLine();
                    break;
            }
        }
        else if (_strings.currentMenu.Equals("customer menu")) {
            switch (_webShopMenu.CurrentChoice)
            {
                case 1:
                    _webShop.currentCustomer.PrintOrders();
                    break;
                case 2:
                    _webShop.currentCustomer.PrintInfo();
                    break;
                case 3:
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
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Not an option.");
                    Console.WriteLine();
                    break;
            }
        }
        else if (_strings.currentMenu.Equals("sort menu"))
        {
            bool back = true;
            switch (_webShopMenu.CurrentChoice)
            {
                case 1:
                    _webShop.bubbleSort("name", false);
                    Console.WriteLine();
                    Console.WriteLine("Wares sorted.");
                    Console.WriteLine();
                    break;
                case 2:
                    _webShop.bubbleSort("name", true);
                    Console.WriteLine();
                    Console.WriteLine("Wares sorted.");
                    Console.WriteLine();
                    break;
                case 3:
                    _webShop.bubbleSort("price", false);
                    Console.WriteLine();
                    Console.WriteLine("Wares sorted.");
                    Console.WriteLine();
                    break;
                case 4:
                    _webShop.bubbleSort("price", true);
                    Console.WriteLine();
                    Console.WriteLine("Wares sorted.");
                    Console.WriteLine();
                    break;
                default:
                    back = false;
                    Console.WriteLine();
                    Console.WriteLine("Not an option.");
                    Console.WriteLine();
                    break;
            }
            if (back)
            {
                _webShopMenu.Options[0] = "See all wares";
                _webShopMenu.Options[1] = "Purchase a ware";
                _webShopMenu.Options[2] = "Sort wares";
                if (_webShop.currentCustomer == null)
                {
                    _webShopMenu.Options[3] = "Login";
                }
                else
                {
                    _webShopMenu.Options[3] = "Logout";
                }
                _webShopMenu.AmountOfOptions = 4;
                _webShopMenu.CurrentChoice = 1;
                _strings.currentMenu = "wares menu";
                _strings.MainMenuWhat = "What would you like to do?";
            }
        }
        else if (_strings.currentMenu.Equals("login menu"))
        {
            switch (_webShopMenu.CurrentChoice)
            {
                case 1:
                    Console.WriteLine("A keyboard appears.");
                    Console.WriteLine("Please input your username.");
                    _webShopMenu.Username = Console.ReadLine();
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine("A keyboard appears.");
                    Console.WriteLine("Please input your password.");
                    _webShopMenu.Password = Console.ReadLine();
                    Console.WriteLine();
                    break;
                case 3:
                    if (_webShopMenu.Username == null || _webShopMenu.Password == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Incomplete data.");
                        Console.WriteLine();
                    }
                    else
                    {
                        bool found = false;
                        foreach (Customer customer in _webShop.customers)
                        {
                            if (_webShopMenu.Username.Equals(customer.Username) && customer.CheckPassword(_webShopMenu.Password))
                            {
                                Console.WriteLine();
                                Console.WriteLine(customer.Username + " logged in.");
                                Console.WriteLine();
                                _webShop.currentCustomer = customer;
                                found = true;
                                _webShopMenu.Options[0] = "See Wares";
                                _webShopMenu.Options[1] = "Customer Info";
                                if (_webShop.currentCustomer == null)
                                {
                                    _webShopMenu.Options[2] = "Login";
                                }
                                else
                                {
                                    _webShopMenu.Options[2] = "Logout";
                                }
                                _strings.MainMenuWhat = "What would you like to do?";
                                _strings.currentMenu = "main menu";
                                _webShopMenu.CurrentChoice = 1;
                                _webShopMenu.AmountOfOptions = 3;
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
                    break;
                case 4:
                    Console.WriteLine("Please write your username.");
                    string newUsername = Console.ReadLine();
                    foreach (Customer customer in _webShop.customers)
                    {
                        if (customer.Username.Equals(_webShopMenu.Username))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Username already exists.");
                            Console.WriteLine();
                            break;
                        }
                    }
                    // Would have liked to be able to quit at any time in here.
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
                                    continue;
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
                                    continue;
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
                                    continue;
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
                                    continue;
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
                                    continue;
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
                                    continue;
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
                    _webShop.customers.Add(newCustomer);
                    _webShop.currentCustomer = newCustomer;
                    Console.WriteLine();
                    Console.WriteLine(newCustomer.Username + " successfully added and is now logged in.");
                    Console.WriteLine();
                    _webShopMenu.Options[0] = "See Wares";
                    _webShopMenu.Options[1] = "Customer Info";
                    if (_webShop.currentCustomer == null)
                    {
                        _webShopMenu.Options[2] = "Login";
                    }
                    else
                    {
                        _webShopMenu.Options[2] = "Logout";
                    }
                    _strings.MainMenuWhat = "What would you like to do?";
                    _strings.currentMenu = "main menu";
                    _webShopMenu.CurrentChoice = 1;
                    _webShopMenu.AmountOfOptions = 3;
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Not an option.");
                    Console.WriteLine();
                    break;
            }
        }
        else if (_strings.currentMenu.Equals("purchase menu"))
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
}