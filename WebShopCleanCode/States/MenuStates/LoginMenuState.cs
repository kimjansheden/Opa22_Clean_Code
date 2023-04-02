using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode.States.MenuStates;

public class LoginMenuState : MenuState
{
    public LoginMenuState(App app, WebShop webShop)
    {
        App = app;
        WebShop = webShop;
        OptionActions = new Dictionary<int, Action>
        {
            { 1, SetUsername },
            { 2, SetPassword },
            { 3, Login },
            { 4, Register }
        };
        Options = new List<string>
        {
            ((DefaultStrings)Strings).Login.Option1,
            ((DefaultStrings)Strings).Login.Option2,
            ((DefaultStrings)Strings).Login.Option3,
            ((DefaultStrings)Strings).Login.Option4
        };
        app.CurrentChoice = 1;
    }
    private void Register()
    {
        Console.WriteLine(((DefaultStrings)Strings).Login.WriteUsername);
        string newUsername = Console.ReadLine();
        foreach (Customer customer in WebShop.Customers)
        {
            if (customer.Username.Equals(App.Username))
            {
                PrintMessageWithPadding(((DefaultStrings)Strings).Login.UsernameExists);
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
                        PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteSomething);
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
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(((DefaultStrings)Strings).Login.WantFirstName);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(((DefaultStrings)Strings).Login.WriteFirstName);
                    firstName = Console.ReadLine();
                    if (firstName.Equals(""))
                    {
                        PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteSomething);
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
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(((DefaultStrings)Strings).Login.WantLastName);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(((DefaultStrings)Strings).Login.WriteLastName);
                    lastName = Console.ReadLine();
                    if (lastName.Equals(""))
                    {
                        PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteSomething);
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
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(((DefaultStrings)Strings).Login.WantEmail);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(((DefaultStrings)Strings).Login.WriteEmail);
                    email = Console.ReadLine();
                    if (email.Equals(""))
                    {
                        PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteSomething);
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
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(((DefaultStrings)Strings).Login.WantAge);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(((DefaultStrings)Strings).Login.WriteAge);
                    string ageString = Console.ReadLine();
                    try
                    {
                        age = int.Parse(ageString);
                    }
                    catch (FormatException e)
                    {
                        PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteNumber);
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
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(((DefaultStrings)Strings).Login.WantAddress);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(((DefaultStrings)Strings).Login.WriteAddress);
                    address = Console.ReadLine();
                    if (address.Equals(""))
                    {
                        PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteSomething);
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
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.YOrN);
        }
        while (true)
        {
            Console.WriteLine(((DefaultStrings)Strings).Login.WantPhone);
            choice = Console.ReadLine();
            if (choice.Equals("y"))
            {
                while (true)
                {
                    Console.WriteLine(((DefaultStrings)Strings).Login.WritePhone);
                    phoneNumber = Console.ReadLine();
                    if (phoneNumber.Equals(""))
                    {
                        PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteSomething);
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
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.YOrN);
        }
    
        Customer newCustomer = new Customer(newUsername, newPassword, firstName, lastName, email, age, address, phoneNumber);
        WebShop.Customers.Add(newCustomer);
        CurrentCustomer = newCustomer;
        App.LoginState = new LoggedInState(WebShop, App);
        PrintMessageWithPadding(newCustomer.Username + " successfully added and is now logged in.");
        ChangeState("MainMenu");
    }

    private void Login()
    {
        if (App.Username == null || App.Password == null)
        {
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.IncompleteData);
        }
        else
        {
            bool found = false;
            foreach (Customer customer in WebShop.Customers)
            {
                if (App.Username.Equals(customer.Username) && customer.CheckPassword(App.Password))
                {
                    PrintMessageWithPadding(customer.Username + " logged in.");
                    CurrentCustomer = customer;
                    found = true;
                    ChangeState("MainMenu");
                    break;
                }
            }
            if (found == false)
            {
                PrintMessageWithPadding(((DefaultStrings)Strings).Login.InvalidCreds);
            }
        }
    }

    private void SetPassword()
    {
        Console.WriteLine(((DefaultStrings)Strings).Login.AKeyBoard);
        Console.WriteLine(((DefaultStrings)Strings).Login.InputPassword);
        App.Password = Console.ReadLine();
        Console.WriteLine();
    }

    private void SetUsername()
    {
        Console.WriteLine(((DefaultStrings)Strings).Login.AKeyBoard);
        Console.WriteLine(((DefaultStrings)Strings).Login.InputUsername);
        App.Username = Console.ReadLine();
        Console.WriteLine();
    }

    protected internal override void DisplayOptions()
    {
        App.SetOptions(Options);
        AmountOfOptions = 4;
        Console.WriteLine(((DefaultStrings)Strings).Login.Menu);
        App.PrintOptions();
    }
    protected override void ChangeState(string state)
    {
        CurrentState = States[state];
        CurrentChoice = 1;
    }
}