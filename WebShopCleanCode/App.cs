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
    
    private readonly List<string> _options;
    private string _username;
    private string _password;
    private string[] _quitCommands;
    private readonly WebShop _webShop;
    private Strings _strings;
    private int _currentChoice;
    private int _amountOfOptions;
    private ICommand _currentCommand;
    private LoginState _loginState;
    private State _currentState;
    private List<State> _stateHistory;

    public string Username
    {
        get => _username;
        set => _username = value;
    }

    public string Password
    {
        get => _password;
        set => _password = value;
    }
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

    public State CurrentState
    {
        get => _currentState;
        set => _currentState = value;
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
        IWebShopFactory webShopFactory = new DefaultWebShopFactory();
        _webShop = webShopFactory.CreateWebShop();
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
        _options.Add(((DefaultStrings)_strings).Main.Option1);
        _options.Add(((DefaultStrings)_strings).Main.Option2);
        _options.Add(((DefaultStrings)_strings).Main.Option3);
        _options.Add(((DefaultStrings)_strings).Main.Option4);
    }

    private void CreateDefaultCommands()
    {
        _quitCommands = ((DefaultStrings)_strings).Quit;

        _commands.Add("left", new LeftCommand(this));
        _commands.Add("l", new LeftCommand(this));

        _commands.Add("right", new RightCommand(this));
        _commands.Add("r", new RightCommand(this));

        _commands.Add("ok", new OkCommand(this));
        _commands.Add("o", new OkCommand(this));
        _commands.Add("k", new OkCommand(this));

        foreach (var quitCommand in _quitCommands)
        {
            _commands.Add(quitCommand, new QuitCommand(this));
        }

        _commands.Add("back", new BackCommand(this));
        _commands.Add("b", new BackCommand(this));
    }

    /// <summary>
    /// Constructor for testing purposes.
    /// </summary>
    /// <param name="loggedInCustomer">True if you want to start with a pre made logged in customer.</param> 
    public App(bool loggedInCustomer) : this()
    {
        if (loggedInCustomer)
        {
            CustomerBuilder customerBuilder = new CustomerBuilder();
            Customer newCustomer = customerBuilder.SetUsername("Test").SetPassword("Test").Build();
            _webShop.Customers.Add(newCustomer);
            _webShop.CurrentCustomer = newCustomer;
            _loginState = _loginStates["LoggedIn"];
            _options[2] = ((DefaultStrings)_strings).LogoutString;
        }
    }
    
    private void CreateWebShop(bool defaultConstructor)
    {
        _amountOfOptions = defaultConstructor ? 3 : _options.Count;
        CurrentCustomer = _webShop.CurrentCustomer;
        _currentChoice = 1;
        _stateHistory = new List<State>();
    }

    public void Run()
    {
        Console.WriteLine(_strings.MainMenuWelcome);

        while (_currentCommand is not QuitCommand)
        {
            DisplayOptions();
            PrintNavigation();

            var input = Console.ReadLine();
            ExecuteCommandIfExists(input!);
        }
    }

    public void DisplayOptions()
    {
        if (_currentState is MenuState menuState) menuState.DisplayOptions();
    }

    public void PrintOptions()
    {
        var optionNum = 1;
        foreach (string option in _options)
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
        for (int i = 0; i < _amountOfOptions; i++)
        {
            Console.Write(i + 1 + "\t");
        }
        Console.WriteLine();
        for (int i = 1; i < _currentChoice; i++)
        {
            Console.Write("\t");
        }
        Console.WriteLine("|");

        Console.WriteLine(_strings.Buttons);
        DisplayUser(CurrentCustomer);
    }
    private void DisplayUser(Customer customer) => Console.WriteLine(customer != null ? $"Current user: {customer.Username}" : "Nobody logged in.");

    private void ExecuteCommandIfExists(string input)
    {
        if (_commands.ContainsKey(input))
        {
            _commands[input].Execute();
            _currentCommand = _commands[input];
        }
        else
        {
            Console.WriteLine(_strings.NotApplicable);
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