using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode.States.MenuStates;

public class LoginMenuState : MenuState
{
    private string? _choice;
    private string? _newPassword;
    private string? _newUsername;
    private string? _firstName;
    private string? _lastName;
    private string? _email;
    private int _age;
    private string? _address;
    private string? _phoneNumber;
    private CustomerBuilder _customerBuilder;
    public LoginMenuState(App app, WebShop webShop) : base(app, webShop)
    {
        _choice = "";
        _age = -1;
        _customerBuilder = new CustomerBuilder();
    }
    private void Register()
    {
        _newUsername = PromptForInput(((DefaultStrings)Strings).Login.WriteUsername);
        CheckIfUserExists();
        
        _choice = PromptForYesOrNo(promptMessage:((DefaultStrings)Strings).Login.WantPassword);
        if (_choice.Equals("y"))
        {
            _newPassword = PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WritePassword,
                wantErrorMessage: true);
        }

        _choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantFirstName);
        if (_choice.Equals("y"))
        {
            _firstName = PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WriteFirstName, wantErrorMessage: true);
        }

        _choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantLastName);
        if (_choice.Equals("y"))
        {
            _lastName = PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WriteLastName,
                wantErrorMessage: true);
        }

        _choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantEmail);
        if (_choice.Equals("y"))
        {
            _email = PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WriteEmail,
                wantErrorMessage: true);
        }

        _choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantAge);
        if (_choice.Equals("y"))
        {
            PromptForAge();
        }

        _choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantAddress);
        if (_choice.Equals("y"))
        {
            _address = PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WriteAddress,
                wantErrorMessage: true);
        }

        _choice = PromptForYesOrNo(((DefaultStrings)Strings).Login.WantPhone);
        if (_choice.Equals("y"))
        {
            _phoneNumber = PromptForInput(promptMessage: ((DefaultStrings)Strings).Login.WritePhone,
                wantErrorMessage: true);
        }

        Customer newCustomer = new Customer(_newUsername, _newPassword, _firstName, _lastName, _email, _age, _address, _phoneNumber);
        WebShop.Customers.Add(newCustomer);
        CurrentCustomer = newCustomer;
        LoginState = new LoggedInState(WebShop, App);
        PrintMessageWithPadding(newCustomer.Username + " successfully added and is now logged in.");
        ChangeState("MainMenu");
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

    protected internal override void DisplayOptions()
    {
        App.SetOptions(Options);
        AmountOfOptions = 4;
        Console.WriteLine(((DefaultStrings)Strings).Login.Menu);
        App.PrintOptions();
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

    protected override void ChangeState(string state)
    {
        CurrentState = States[state];
        CurrentChoice = 1;
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
    private void PromptForAge()
    {
        Console.WriteLine(((DefaultStrings)Strings).Login.WriteAge);
        bool success;
        do
        {
            success = int.TryParse(Console.ReadLine(), out _age);
            if (!success)
            {
                PrintMessageWithPadding(((DefaultStrings)Strings).Login.WriteNumber);    
            }
        } while (!success);
    }
}