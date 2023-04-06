using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Commands;
using WebShopCleanCode.Factories;
using WebShopCleanCode.Helpers;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class App : IApp
{
    private Dictionary<string, ICommand> _commands;
    private Dictionary<string, MenuState> _menuStates;
    private Dictionary<string, LoginState> _loginStates;
    private Dictionary<string, IMenuStateFactory> _menuStateFactories;
    private Dictionary<string, ILoginStateFactory> _loginStateFactories;
    
    private List<string> _options;
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
    private readonly IMenuManager _menuManager;
    private readonly ICommandExecutor _commandExecutor;
    private readonly IOptionsManager _optionsManager;

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

    public Customer CurrentCustomer
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

    public Dictionary<string, ICommand> Commands
    {
        get => _commands;
        set => _commands = value;
    }

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

    public List<string> Options
    {
        get => _options;
        set => _options = value;
    }

    public ICommand CurrentCommand
    {
        get => _currentCommand;
        set => _currentCommand = value;
    }

    /// <summary>
    /// Custom constructor.
    /// </summary>
    /// <param name="webShopFactory"></param>
    /// <param name="menuManager"></param>
    /// <param name="optionsManager"></param>
    /// <param name="commandExecutor"></param>
    /// <param name="strings"></param>
    /// <param name="menuStateFactories"></param>
    /// <param name="loginStateFactories"></param>
    /// <param name="commands"></param>
    /// <param name="quitCommands">Enter one och more quit commands.</param>
    /// <param name="startMenuState">Enter the Start Menu State as a string. It should be one of the keys in your menuStates.</param>
    /// <param name="startLoginState">Enter the Start Login State as a string. It should be one of the keys in your loginStates.</param>
    public App(IWebShopFactory webShopFactory, IMenuManager menuManager, IOptionsManager optionsManager, ICommandExecutor commandExecutor, Strings strings, Dictionary<string, IMenuStateFactory> menuStateFactories, Dictionary<string, ILoginStateFactory> loginStateFactories, Dictionary<string, ICommand> commands, string[] quitCommands, string startMenuState, string startLoginState)
    {
        _webShop = webShopFactory.CreateWebShop();
        
        CreateWebShop();
        
        _menuManager = menuManager.Initialize(this);
        _optionsManager = optionsManager.Initialize(this);
        _commandExecutor = commandExecutor.Initialize(this);
        _strings = strings;
        _menuStateFactories = menuStateFactories;
        _loginStateFactories = loginStateFactories;
        _commands = commands;
        _quitCommands = quitCommands;

        AddQuitCommands();
        AddMenuStates();
        AddLoginStates();
        InitializeCommands();

        _currentState = _menuStates[startMenuState];
        _loginState = _loginStates[startLoginState];

    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public App()
    {
        IWebShopFactory webShopFactory = new DefaultWebShopFactory();
        _menuManager = new MenuManager().Initialize(this);
        _optionsManager = new OptionsManager().Initialize(this);
        _commandExecutor = new CommandExecutor().Initialize(this);
        _webShop = webShopFactory.CreateWebShop();
        _strings = new DefaultStrings();
        _commands = new Dictionary<string, ICommand>();
        CreateWebShop();
        
        CreateDefaultCommands();
        CreateDefaultStates();
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
    
    public void Run()
    {
        Console.WriteLine(_strings.MainMenuWelcome);

        while (_currentCommand is not QuitCommand)
        {
            DisplayOptions();
            _menuManager.PrintNavigation();

            var input = Console.ReadLine();
            _commandExecutor.ExecuteCommandIfExists(input!);
        }
    }

    public void DisplayOptions()
    {
        _optionsManager.DisplayOptions();
    }

    public void PrintOptions()
    {
        _optionsManager.PrintOptions();
    }

    public void ClearAllOptions()
    {
        _optionsManager.ClearAllOptions();
    }
    public void SetOptions(List<string> newOptions)
    {
        _optionsManager.SetOptions(newOptions);
    }
    
    private void InitializeCommands()
    {
        foreach (var command in _commands)
        {
            command.Value.Initialize(this);
        }
    }

    private void CreateDefaultStates()
    {
        _menuStateFactories = new Dictionary<string, IMenuStateFactory>();
        _menuStateFactories.Add("CustomerMenu", new CustomerInfoMenuStateFactory());
        _menuStateFactories.Add("LoginMenu", new LoginMenuStateFactory());
        _menuStateFactories.Add("MainMenu", new MainMenuStateFactory());
        _menuStateFactories.Add("PurchaseMenu", new PurchaseMenuStateFactory());
        _menuStateFactories.Add("SortMenu", new SortMenuStateFactory());
        _menuStateFactories.Add("WaresMenu", new WaresMenuStateFactory());
        
        _loginStateFactories = new Dictionary<string, ILoginStateFactory>();
        _loginStateFactories.Add("LoggedIn", new LoggedInStateFactory());
        _loginStateFactories.Add("LoggedOut", new LoggedOutStateFactory());
        
        AddLoginStates();
        AddMenuStates();
        
        _currentState = _menuStates["MainMenu"];
        _loginState = _loginStates["LoggedOut"];
    }

    private void AddLoginStates()
    {
        foreach (var factory in _loginStateFactories)
        {
            _loginStates.Add(factory.Key, factory.Value.CreateState(this, _webShop));
        }
    }

    private void CreateDefaultCommands()
    {
        _quitCommands = ((DefaultStrings)_strings).Quit;

        _commands.Add("left", new LeftCommand());
        _commands.Add("l", new LeftCommand());

        _commands.Add("right", new RightCommand());
        _commands.Add("r", new RightCommand());

        _commands.Add("ok", new OkCommand());
        _commands.Add("o", new OkCommand());
        _commands.Add("k", new OkCommand());
        
        _commands.Add("back", new BackCommand());
        _commands.Add("b", new BackCommand());
        
        AddQuitCommands();
        InitializeCommands();
    }

    private void AddQuitCommands()
    {
        foreach (var quitCommand in _quitCommands)
        {
            _commands.Add(quitCommand, new QuitCommand());
        }
    }

    private void CreateWebShop()
    {
        _options = new List<string>();
        CurrentCustomer = _webShop.CurrentCustomer;
        _stateHistory = new List<State>();
        _menuStates = new Dictionary<string, MenuState>();
        _loginStates = new Dictionary<string, LoginState>();
    }
    private void AddMenuStates()
    {
        foreach (var factory in _menuStateFactories)
        {
            _menuStates.Add(factory.Key, factory.Value.CreateState(this, _webShop));
        }
    }
}