using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class WebShop
    {
        bool running = true;
        Database database = new Database();
        List<Product> products = new List<Product>();
        List<Customer> customers = new List<Customer>();
        private Strings _strings = new Strings();

        public int currentChoice = 1;
        public int amountOfOptions = 3;

        string username = null;
        string password = null;
        public Customer currentCustomer;

        public WebShop()
        {
            products = database.GetProducts();
            customers = database.GetCustomers();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the WebShop!");
            while (running)
            {
                Console.WriteLine(_strings.Info);
                
                if (_strings.currentMenu.Equals("purchase menu"))
                {
                    for (int i = 0; i < amountOfOptions; i++)
                    {
                        Console.WriteLine(i + 1 + ": " + products[i].Name + ", " + products[i].Price + "kr");
                    }
                    Console.WriteLine("Your funds: " + currentCustomer.Funds);
                }
                else
                {
                    Console.WriteLine("1: " + _strings.Option1);
                    Console.WriteLine("2: " + _strings.Option2);
                    if (amountOfOptions > 2)
                    {
                        Console.WriteLine("3: " + _strings.Option3);
                    }
                    if (amountOfOptions > 3)
                    {
                        Console.WriteLine("4: " + _strings.Option4);
                    }
                }

                for (int i = 0; i < amountOfOptions; i++)
                {
                    Console.Write(i + 1 + "\t");
                }
                Console.WriteLine();
                for (int i = 1; i < currentChoice; i++)
                {
                    Console.Write("\t");
                }
                Console.WriteLine("|");

                Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
                if (currentCustomer != null)
                {
                    Console.WriteLine("Current user: " + currentCustomer.Username);
                }
                else
                {
                    Console.WriteLine("Nobody logged in.");
                }

                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "left":
                    case "l":
                        if (currentChoice > 1)
                        {
                            currentChoice--;
                        }
                        break;
                    case "right":
                    case "r":
                        if (currentChoice < amountOfOptions)
                        {
                            currentChoice++;
                        }
                        break;
                    case "ok":
                    case "k":
                    case "o":
                        if (_strings.currentMenu.Equals("main menu"))
                        {
                            switch (currentChoice)
                            {
                                case 1:
                                    _strings.Option1 = "See all wares";
                                    _strings.Option2 = "Purchase a ware";
                                    _strings.Option3 = "Sort wares";
                                    if (currentCustomer == null)
                                    {
                                        _strings.Option4 = "Login";
                                    }
                                    else
                                    {
                                        _strings.Option4 = "Logout";
                                    }
                                    amountOfOptions = 4;
                                    currentChoice = 1;
                                    _strings.currentMenu = "wares menu";
                                    _strings.Info = "What would you like to do?";
                                    break;
                                case 2:
                                    if (currentCustomer != null)
                                    {
                                        _strings.Option1 = "See your orders";
                                        _strings.Option2 = "Set your info";
                                        _strings.Option3 = "Add funds";
                                        _strings.Option4 = "";
                                        amountOfOptions = 3;
                                        currentChoice = 1;
                                        _strings.Info = "What would you like to do?";
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
                                    if (currentCustomer == null)
                                    {
                                        _strings.Option1 = "Set Username";
                                        _strings.Option2 = "Set Password";
                                        _strings.Option3 = "Login";
                                        _strings.Option4 = "Register";
                                        amountOfOptions = 4;
                                        currentChoice = 1;
                                        _strings.Info = "Please submit username and password.";
                                        username = null;
                                        password = null;
                                        _strings.currentMenu = "login menu";
                                    }
                                    else
                                    {
                                        _strings.Option3 = "Login";
                                        Console.WriteLine();
                                        Console.WriteLine(currentCustomer.Username + " logged out.");
                                        Console.WriteLine();
                                        currentChoice = 1;
                                        currentCustomer = null;
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
                            switch (currentChoice)
                            {
                                case 1:
                                    currentCustomer.PrintOrders();
                                    break;
                                case 2:
                                    currentCustomer.PrintInfo();
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
                                            currentCustomer.Funds += amount;
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
                            switch (currentChoice)
                            {
                                case 1:
                                    bubbleSort("name", false);
                                    Console.WriteLine();
                                    Console.WriteLine("Wares sorted.");
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    bubbleSort("name", true);
                                    Console.WriteLine();
                                    Console.WriteLine("Wares sorted.");
                                    Console.WriteLine();
                                    break;
                                case 3:
                                    bubbleSort("price", false);
                                    Console.WriteLine();
                                    Console.WriteLine("Wares sorted.");
                                    Console.WriteLine();
                                    break;
                                case 4:
                                    bubbleSort("price", true);
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
                                _strings.Option1 = "See all wares";
                                _strings.Option2 = "Purchase a ware";
                                _strings.Option3 = "Sort wares";
                                if (currentCustomer == null)
                                {
                                    _strings.Option4 = "Login";
                                }
                                else
                                {
                                    _strings.Option4 = "Logout";
                                }
                                amountOfOptions = 4;
                                currentChoice = 1;
                                _strings.currentMenu = "wares menu";
                                _strings.Info = "What would you like to do?";
                            }
                        }
                        else if (_strings.currentMenu.Equals("wares menu"))
                        {
                            switch (currentChoice)
                            {
                                case 1:
                                    Console.WriteLine();
                                    foreach (Product product in products)
                                    {
                                        product.PrintInfo();
                                    }
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    if (currentCustomer != null)
                                    {
                                        _strings.currentMenu = "purchase menu";
                                        _strings.Info = "What would you like to purchase?";
                                        currentChoice = 1;
                                        amountOfOptions = products.Count;
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("You must be logged in to purchase wares.");
                                        Console.WriteLine();
                                        currentChoice = 1;
                                    }
                                    break;
                                case 3:
                                    _strings.Option1 = "Sort by name, descending";
                                    _strings.Option2 = "Sort by name, ascending";
                                    _strings.Option3 = "Sort by price, descending";
                                    _strings.Option4 = "Sort by price, ascending";
                                    _strings.Info = "How would you like to sort them?";
                                    _strings.currentMenu = "sort menu";
                                    currentChoice = 1;
                                    amountOfOptions = 4;
                                    break;
                                case 4:
                                    if (currentCustomer == null)
                                    {
                                        _strings.Option1 = "Set Username";
                                        _strings.Option2 = "Set Password";
                                        _strings.Option3 = "Login";
                                        _strings.Option4 = "Register";
                                        amountOfOptions = 4;
                                        _strings.Info = "Please submit username and password.";
                                        currentChoice = 1;
                                        _strings.currentMenu = "login menu";
                                    }
                                    else
                                    {
                                        _strings.Option4 = "Login";
                                        Console.WriteLine();
                                        Console.WriteLine(currentCustomer.Username + " logged out.");
                                        Console.WriteLine();
                                        currentCustomer = null;
                                        currentChoice = 1;
                                    }
                                    break;
                                case 5:
                                    break;
                                default:
                                    Console.WriteLine();
                                    Console.WriteLine("Not an option.");
                                    Console.WriteLine();
                                    break;
                            }
                        }
                        else if (_strings.currentMenu.Equals("login menu"))
                        {
                            switch (currentChoice)
                            {
                                case 1:
                                    Console.WriteLine("A keyboard appears.");
                                    Console.WriteLine("Please input your username.");
                                    username = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine("A keyboard appears.");
                                    Console.WriteLine("Please input your password.");
                                    password = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 3:
                                    if (username == null || password == null)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Incomplete data.");
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        bool found = false;
                                        foreach (Customer customer in customers)
                                        {
                                            if (username.Equals(customer.Username) && customer.CheckPassword(password))
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine(customer.Username + " logged in.");
                                                Console.WriteLine();
                                                currentCustomer = customer;
                                                found = true;
                                                _strings.Option1 = "See Wares";
                                                _strings.Option2 = "Customer Info";
                                                if (currentCustomer == null)
                                                {
                                                    _strings.Option3 = "Login";
                                                }
                                                else
                                                {
                                                    _strings.Option3 = "Logout";
                                                }
                                                _strings.Info = "What would you like to do?";
                                                _strings.currentMenu = "main menu";
                                                currentChoice = 1;
                                                amountOfOptions = 3;
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
                                    foreach (Customer customer in customers)
                                    {
                                        if (customer.Username.Equals(username))
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Username already exists.");
                                            Console.WriteLine();
                                            break;
                                        }
                                    }
                                    // Would have liked to be able to quit at any time in here.
                                    choice = "";
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
                                    customers.Add(newCustomer);
                                    currentCustomer = newCustomer;
                                    Console.WriteLine();
                                    Console.WriteLine(newCustomer.Username + " successfully added and is now logged in.");
                                    Console.WriteLine();
                                    _strings.Option1 = "See Wares";
                                    _strings.Option2 = "Customer Info";
                                    if (currentCustomer == null)
                                    {
                                        _strings.Option3 = "Login";
                                    }
                                    else
                                    {
                                        _strings.Option3 = "Logout";
                                    }
                                    _strings.Info = "What would you like to do?";
                                    _strings.currentMenu = "main menu";
                                    currentChoice = 1;
                                    amountOfOptions = 3;
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
                            int index = currentChoice - 1;
                            Product product = products[index];
                            if (product.InStock())
                            {
                                if (currentCustomer.CanAfford(product.Price))
                                {
                                    currentCustomer.Funds -= product.Price;
                                    product.NrInStock--;
                                    currentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
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
                        break;
                    case "back":
                    case "b":
                        if (_strings.currentMenu.Equals("main menu"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("You're already on the main menu.");
                            Console.WriteLine();
                        }
                        else if (_strings.currentMenu.Equals("purchase menu"))
                        {
                            _strings.Option1 = "See all wares";
                            _strings.Option2 = "Purchase a ware";
                            _strings.Option3 = "Sort wares";
                            if (currentCustomer == null)
                            {
                                _strings.Option4 = "Login";
                            }
                            else
                            {
                                _strings.Option4 = "Logout";
                            }
                            amountOfOptions = 4;
                            currentChoice = 1;
                            _strings.currentMenu = "wares menu";
                            _strings.Info = "What would you like to do?";
                        }
                        else
                        {
                            _strings.Option1 = "See Wares";
                            _strings.Option2 = "Customer Info";
                            if (currentCustomer == null)
                            {
                                _strings.Option3 = "Login";
                            }
                            else
                            {
                                _strings.Option3 = "Logout";
                            }
                            _strings.Info = "What would you like to do?";
                            _strings.currentMenu = "main menu";
                            currentChoice = 1;
                            amountOfOptions = 3;
                        }
                        break;
                    case "quit":
                    case "q":
                        Console.WriteLine("The console powers down. You are free to leave.");
                        return;
                    default:
                        Console.WriteLine("That is not an applicable option.");
                        break;
                }
            }
        }
        private void bubbleSort(string variable, bool ascending)
        {
            if (variable.Equals("name")) {
                int length = products.Count;
                for(int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (products[j].Name.CompareTo(products[j + 1].Name) < 0)
                            {
                                Product temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (products[j].Name.CompareTo(products[j + 1].Name) > 0)
                            {
                                Product temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
            else if (variable.Equals("price"))
            {
                int length = products.Count;
                for (int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (products[j].Price > products[j + 1].Price)
                            {
                                Product temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (products[j].Price < products[j + 1].Price)
                            {
                                Product temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
        }
    }
}
