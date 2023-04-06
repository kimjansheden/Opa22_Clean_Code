using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Commands;
using WebShopCleanCode.Factories;
using WebShopCleanCode.Helpers;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.LoginStates;

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
    /// <param name="loginStateFactories"></param>
    /// <param name="commands"></param>
    /// <param name="options"></param>
    /// <param name="quitCommands">Enter one och more quit commands.</param>
    /// <param name="startState"></param>
    /// <param name="startLoginState"></param>
    /// <param name="menuMenuStateFactories"></param>
    public App(IWebShopFactory webShopFactory, IMenuManager menuManager, IOptionsManager optionsManager, ICommandExecutor commandExecutor, Strings strings, Dictionary<string, IMenuStateFactory> menuMenuStateFactories, Dictionary<string, ILoginStateFactory> loginStateFactories, Dictionary<string, ICommand> commands, List<string> options, string[] quitCommands, MenuState startState, LoginState startLoginState)
    {
        CreateWebShop(defaultConstructor: false);
        _webShop = webShopFactory.CreateWebShop();
        _menuManager = menuManager.Initialize(this);
        _optionsManager = optionsManager.Initialize(this);
        _commandExecutor = commandExecutor.Initialize(this);
        _strings = strings;
        _menuStateFactories = menuMenuStateFactories;
        _loginStateFactories = loginStateFactories;
        _commands = commands;
        _options = options;
        _quitCommands = quitCommands;
        _currentState = startState;
        _loginState = startLoginState;

        AddMenuStates();
        AddLoginStates();
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
        _options = new List<string>();
        CreateWebShop(defaultConstructor: true);

        CreateDefaultOptions();
        CreateDefaultCommands();
        CreateDefaultStates();
    }

    private void CreateDefaultStates()
    {
        _menuStateFactories = new Dictionary<string, IMenuStateFactory>();
        _menuStateFactories.Add("CustomerMenu", new CustomerInfoMenuMenuStateFactory());
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
}