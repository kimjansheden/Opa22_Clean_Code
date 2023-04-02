using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Commands;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.LoginStates;
using WebShopCleanCode.States.MenuStates;

namespace WebShopCleanCode;

public class App : IMenu
{
    private readonly Dictionary<string, ICommand> _commands;
    private Dictionary<string, MenuState> _menuStates;
    private Dictionary<string, LoginState> _loginStates;
    
    private List<string> _options;
    private string[] _quitCommands;
    private readonly WebShop _webShop;
    private Strings _strings;
    private int _currentChoice;
    private int _amountOfOptions;
    public string Username = null;
    public string Password = null;
    private ICommand _currentCommand;
    private State _loginState;
    private State _previousState;
    private State _currentMenuState;
    private List<State> _stateHistory;
    public int CurrentChoice
    {
        get => _currentChoice;
        set => _currentChoice = value;
    }

    public int AmountOfOptions
    {
        get => _amountOfOptions;
        set => _amountOfOptions = value;
    }

    private List<string> Options
    {
        get => _options;
        set => _options = value;
    }

    private Customer CurrentCustomer
    {
        get => _webShop.CurrentCustomer;
        set => _webShop.CurrentCustomer = value;
    }

    public Strings Strings
    {
        get => _strings;
        set => _strings = value;
    }

    private ICommand CurrentCommand
    {
        get => _currentCommand;
        set => _currentCommand = value;
    }

    public State CurrentState
    {
        get => _currentMenuState;
        set => _currentMenuState = value;
    }

    public State PreviousState
    {
        get => _previousState;
        set => _previousState = value;
    }

    public Dictionary<string, ICommand> Commands => _commands;

    public Dictionary<string, MenuState> MenuStates
    {
        get => _menuStates;
        set => _menuStates = value;
    }

    public List<State> StateHistory
    {
        get => _stateHistory;
        set => _stateHistory = value;
    }

    public Dictionary<string, LoginState> LoginStates
    {
        get => _loginStates;
        set => _loginStates = value;
    }
    public State LoginState
    {
        get => _loginState;
        set => _loginState = value;
    }

    /// <summary>
    /// Custom constructor.
    /// </summary>
    /// <param name="webShop"></param>
    /// <param name="strings"></param>
    /// <param name="commands"></param>
    /// <param name="options"></param>
    /// <param name="quitCommands">Enter one och more quit commands.</param>
    /// <param name="startMenuState"></param>
    /// <param name="startLoginState"></param>
    /// <param name="menuStates"></param>
    /// <param name="loginStates"></param>
    public App(WebShop webShop, Strings strings, Dictionary<string, ICommand> commands, List<string> options, string[] quitCommands, MenuState startMenuState, LoginState startLoginState, Dictionary<string, MenuState> menuStates, Dictionary<string, LoginState> loginStates)
    {
        _webShop = webShop;
        _strings = strings;
        _commands = commands;
        _options = options;
        _menuStates = menuStates;
        _loginStates = loginStates;
        _quitCommands = quitCommands;
        _currentMenuState = startMenuState;
        _loginState = startLoginState;
        CreateWebShop(defaultConstructor: false);
    }
    
    /// <summary>
    /// Default constructor.
    /// </summary>
    public App()
    {
        _webShop = new DefaultWebShop();
        _strings = new DefaultStrings();
        _commands = new Dictionary<string, ICommand>();
        _options = new List<string>();
        CreateWebShop(defaultConstructor: true);

        CreateDefaultOptions();
        CreateDefaultCommands();
        CreateDefaultStates();
    }

    private void CreateDefaultStates()
    {
        _loginStates = new Dictionary<string, LoginState>();
        _loginStates.Add("LoggedIn", new LoggedInState(_webShop, this));
        _loginStates.Add("LoggedOut", new LoggedOutState(this));
        
        _menuStates = new Dictionary<string, MenuState>();
        _menuStates.Add("CustomerMenu", new CustomerInfoMenuState(this, _webShop));
        _menuStates.Add("LoginMenu", new LoginMenuState(this, _webShop));
        _menuStates.Add("MainMenu", new MainMenuState(this, _webShop));
        _menuStates.Add("PurchaseMenu", new PurchaseMenuState(this, _webShop));
        _menuStates.Add("SortMenu", new SortMenuState(this, _webShop));
        _menuStates.Add("WaresMenu", new WaresMenuState(this, _webShop));
        
        _currentMenuState = _menuStates["MainMenu"];
        _loginState = _loginStates["LoggedOut"];
    }

    private void CreateDefaultOptions()
    {
        _options.Add(((DefaultStrings)Strings).Main.Option1);
        _options.Add(((DefaultStrings)Strings).Main.Option2);
        _options.Add(((DefaultStrings)Strings).Main.Option3);
        _options.Add(((DefaultStrings)Strings).Main.Option4);
    }

    private void CreateDefaultCommands()
    {
        _quitCommands = ((DefaultStrings)Strings).Quit;

        Commands.Add("left", new LeftCommand(this));
        Commands.Add("l", new LeftCommand(this));

        Commands.Add("right", new RightCommand(this));
        Commands.Add("r", new RightCommand(this));

        Commands.Add("ok", new OkCommand(this));
        Commands.Add("o", new OkCommand(this));
        Commands.Add("k", new OkCommand(this));

        foreach (var quitCommand in _quitCommands)
        {
            Commands.Add(quitCommand, new QuitCommand(this));
        }

        Commands.Add("back", new BackCommand(this));
        Commands.Add("b", new BackCommand(this));
    }

    /// <summary>
    /// Constructor for testing purposes.
    /// </summary>
    /// <param name="loggedInCustomer">True if you want to start with a pre made logged in customer.</param> 
    public App(bool loggedInCustomer) : this()
    {
        if (loggedInCustomer)
        {
            Customer newCustomer = new Customer(
                username: "Test",
                password: "Test",
                firstName: null,
                lastName: null,
                email: null,
                age: -1,
                address: null,
                phoneNumber: null);
            
            _webShop.Customers.Add(newCustomer);
            _webShop.CurrentCustomer = newCustomer;
            _loginState = _loginStates["LoggedIn"];
            Options[2] = ((DefaultStrings)Strings).LogoutString;
        }
    }
    
    private void CreateWebShop(bool defaultConstructor)
    {
        AmountOfOptions = defaultConstructor ? 3 : _options.Count;
        CurrentCustomer = _webShop.CurrentCustomer;
        CurrentChoice = 1;
        PreviousState = _currentMenuState;
        _stateHistory = new List<State>();
    }

    public void Run()
    {
        Console.WriteLine(_strings.MainMenuWelcome);
        string input = "";
        
        while (CurrentCommand is not QuitCommand)
        {
            DisplayOptions();
            PrintNavigation();

            input = Console.ReadLine();
            ExecuteCommandIfExists(input);
        }
    }

    public void DisplayOptions()
    {
        if (CurrentState is MenuState menuState) menuState.DisplayOptions();
    }

    public void PrintOptions()
    {
        var optionNum = 1;
        foreach (string option in Options)
        {
            if(!string.IsNullOrEmpty(option))
                Console.WriteLine(optionNum++ + ": " + option);
            if (_amountOfOptions <= 3 && optionNum == 4)
            {
                return;
            }
        }
    }

    private void PrintNavigation()
    {
        for (int i = 0; i < AmountOfOptions; i++)
        {
            Console.Write(i + 1 + "\t");
        }
        Console.WriteLine();
        for (int i = 1; i < CurrentChoice; i++)
        {
            Console.Write("\t");
        }
        Console.WriteLine("|");

        Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
        DisplayUser(CurrentCustomer);
    }
    private void DisplayUser(Customer customer) => Console.WriteLine(customer != null ? $"Current user: {customer.Username}" : "Nobody logged in.");

    private void ExecuteCommandIfExists(string input)
    {
        if (Commands.ContainsKey(input))
        {
            Commands[input].Execute();
            CurrentCommand = Commands[input];
        }
        else
        {
            Console.WriteLine("That is not an applicable option.");
        }
    }

    public void ClearAllOptions()
    {
        for (int i = 0; i < _options.Count; i++)
        {
            _options[i] = "";
        }
    }
    public void SetOptions(List<string> newOptions)
    {
        _options.Clear();
        _options.AddRange(newOptions);
    }
}