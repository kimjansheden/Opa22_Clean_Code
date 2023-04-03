using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.MenuStates;

public class LoginMenuState : MenuState
{
    private readonly CustomerBuilder _customerBuilder;
    public LoginMenuState(App app, WebShop webShop) : base(app, webShop)
    {
        // I decided to use the Builder Design Pattern here to separate the construction of the Customer from the interaction with the user.
        _customerBuilder = new CustomerBuilder();
    }
    protected internal override void Initialize()
    {
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
        CurrentChoice = 1;
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
    private void Register()
    {
        SetCustomerInfo();
        AddNewCustomer();
    }

    private void AddNewCustomer()
    {
        Customer newCustomer = _customerBuilder.Build();
        WebShop.Customers.Add(newCustomer);
        CurrentCustomer = newCustomer;
        LoginState = App.LoginStates["LoggedIn"];
        PrintMessageWithPadding(newCustomer.Username + " successfully added and is now logged in.");
        ChangeState("MainMenu");
    }

    private void SetCustomerInfo()
    {
        _customerBuilder.SetUsername(PromptForInput(((DefaultStrings)Strings).Login.WriteUsername));
        CheckIfUserExists();
        var choice = "";

        choice = PromptForYesOrNo(promptMessage: ((DefaultStrings)Strings).Login.WantPassword);
        if (choice.Equals("y"))
        {
            _customerBuilder.SetPassword(PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WritePassword,
                wantErrorMessage: true)!);
        }

        choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantFirstName);
        if (choice.Equals("y"))
        {
            _customerBuilder.SetFirstName(PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WriteFirstName,
                wantErrorMessage: true)!);
        }

        choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantLastName);
        if (choice.Equals("y"))
        {
            _customerBuilder.SetLastName(PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WriteLastName,
                wantErrorMessage: true)!);
        }

        choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantEmail);
        if (choice.Equals("y"))
        {
            _customerBuilder.SetEmail(PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WriteEmail,
                wantErrorMessage: true)!);
        }

        choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantAge);
        if (choice.Equals("y"))
        {
            _customerBuilder.SetAge(PromptForAge());
        }

        choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantAddress);
        if (choice.Equals("y"))
        {
            _customerBuilder.SetAddress(PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WriteAddress,
                wantErrorMessage: true)!);
        }

        choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantPhone);
        if (choice.Equals("y"))
        {
            _customerBuilder.SetPhoneNumber(PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WritePhone,
                wantErrorMessage: true)!);
        }
    }

    private void CheckIfUserExists()
    {
        foreach (Customer customer in WebShop.Customers)
        {
            if (customer.Username.Equals(App.Username))
            {
                PrintMessageWithPadding(((DefaultStrings)Strings).Login.UsernameExists);
                break;
            }
        }
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

    /// <summary>
    /// Prompt the user for input. If you only write a promptMessage, the user input can be null. If you also want an errorMessage, the user input can't be null. The user will have to write something, otherwise the error message will loop.
    /// </summary>
    /// <param name="promptMessage">What you want to tell the user to do.</param>
    /// <param name="wantErrorMessage">Optional. Set to true if the user has to actually write something.</param>
    /// <returns>The user's input</returns>
    private string? PromptForInput(string promptMessage, bool wantErrorMessage = false)
    {
        Console.WriteLine(promptMessage);
        var errorMessage = ((DefaultStrings)Strings).Login.WriteSomething;
        string input;
        do
        {
            input = Console.ReadLine();
            if (!wantErrorMessage)
            {
                return input;
            }
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine(errorMessage);
            }
        } while (string.IsNullOrEmpty(input));
        
        return input;
    }
    /// <summary>
    /// Prompt the user for yes or no. The user input can't be null. The user will have to write yes or no, otherwise the error message will loop.
    /// </summary>
    /// <param name="promptMessage">What you want to tell the user to do.</param>
    /// <returns>y or n</returns>
    private string PromptForYesOrNo(string promptMessage)
    {
        Console.WriteLine(promptMessage);
        string input;
        do
        {
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input is not ("n" or "y"))
            {
                Console.WriteLine(((DefaultStrings)Strings).Login.YOrN);
            }
        } while (string.IsNullOrEmpty(input) || input is not ("n" or "y"));
        
        return input;
    }
    private int PromptForAge()
    {
        Console.WriteLine(((DefaultStrings)Strings).Login.WriteAge);
        bool success;
        int age;
        do
        {
            success = int.TryParse(Console.ReadLine(), out age);
            if (!success)
            {
                PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteNumber);    
            }
        } while (!success);

        return age;
    }
}