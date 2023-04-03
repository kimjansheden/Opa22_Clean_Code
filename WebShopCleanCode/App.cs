using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Commands;
using WebShopCleanCode.Factories;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode;

public class App : IMenu
{
    private readonly Dictionary<string, ICommand> _commands;
    private Dictionary<string, MenuState> _menuStates;
    private Dictionary<string, LoginState> _loginStates;
    private Dictionary<string, IMenuStateFactory> _stateFactories;
    
    private List<string> _options;
    private string[] _quitCommands;
    private readonly WebShop _webShop;
    private readonly IWebShopFactory _webShopFactory;
    private Strings _strings;
    private int _currentChoice;
    private int _amountOfOptions;
    public string Username = null;
    public string Password = null;
    private ICommand _currentCommand;
    private LoginState _loginState;
    private State _previousState;
    private State _currentState;
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
        get => _currentState;
        set => _currentState = value;
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
    public LoginState LoginState
    {
        get => _loginState;
        set => _loginState = value;
    }

    public Dictionary<string, IMenuStateFactory> StateFactories
    {
        get => _stateFactories;
        set => _stateFactories = value;
    }

    /// <summary>
    /// Custom constructor.
    /// </summary>
    /// <param name="webShopFactory"></param>
    /// <param name="strings"></param>
    /// <param name="commands"></param>
    /// <param name="options"></param>
    /// <param name="quitCommands">Enter one och more quit commands.</param>
    /// <param name="startState"></param>
    /// <param name="startLoginState"></param>
    /// <param name="menuStates"></param>
    /// <param name="loginStates"></param>
    /// <param name="menuStateFactories"></param>
    public App(IWebShopFactory webShopFactory, Strings strings, Dictionary<string, ICommand> commands, List<string> options, string[] quitCommands, MenuState startState, LoginState startLoginState, Dictionary<string, MenuState> menuStates, Dictionary<string, LoginState> loginStates, Dictionary<string, IMenuStateFactory> menuStateFactories)
    {
        _webShopFactory = webShopFactory;
        _webShop = webShopFactory.CreateWebShop();
        _strings = strings;
        _commands = commands;
        _options = options;
        _menuStates = menuStates;
        _loginStates = loginStates;
        _quitCommands = quitCommands;
        _currentState = startState;
        _loginState = startLoginState;
        _stateFactories = menuStateFactories;
        CreateWebShop(defaultConstructor: false);
    }
    
    /// <summary>
    /// Default constructor.
    /// </summary>
    public App()
    {
        _webShopFactory = new DefaultWebShopFactory();
        _webShop = _webShopFactory.CreateWebShop();
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
        // In order to avoid calling a virtual member in the constructor, I have implemented the Factory Method Design Pattern.
        _stateFactories = new Dictionary<string, IMenuStateFactory>();
        _stateFactories.Add("CustomerMenu", new CustomerInfoMenuMenuStateFactory());
        _stateFactories.Add("LoginMenu", new LoginMenuStateFactory());
        _stateFactories.Add("MainMenu", new MainMenuStateFactory());
        _stateFactories.Add("PurchaseMenu", new PurchaseMenuStateFactory());
        _stateFactories.Add("SortMenu", new SortMenuStateFactory());
        _stateFactories.Add("WaresMenu", new WaresMenuStateFactory());
        
        _loginStates = new Dictionary<string, LoginState>();
        _loginStates.Add("LoggedIn", new LoggedInState(_webShop, this));
        _loginStates.Add("LoggedOut", new LoggedOutState(this));
        
        _menuStates = new Dictionary<string, MenuState>();
        _menuStates.Add("CustomerMenu", _stateFactories["CustomerMenu"].CreateState(this, _webShop));
        _menuStates.Add("LoginMenu", _stateFactories["LoginMenu"].CreateState(this, _webShop));
        _menuStates.Add("MainMenu", _stateFactories["MainMenu"].CreateState(this, _webShop));
        _menuStates.Add("PurchaseMenu", _stateFactories["PurchaseMenu"].CreateState(this, _webShop));
        _menuStates.Add("SortMenu", _stateFactories["SortMenu"].CreateState(this, _webShop));
        _menuStates.Add("WaresMenu", _stateFactories["WaresMenu"].CreateState(this, _webShop));
        
        _currentState = _menuStates["MainMenu"];
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
        PreviousState = _currentState;
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