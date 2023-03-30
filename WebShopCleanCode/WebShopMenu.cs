using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Commands;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.LoginStates;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode;

public class WebShopMenu : IMenu
{
    private readonly Dictionary<string, ICommand> _commands;
    private Dictionary<StatesEnum, IMenuState> _menuStates;
    private Dictionary<StatesEnum, ILoginState> _loginStates;
    
    private List<string> _options;
    private string[] _quitCommands;
    private readonly WebShop _webShop;
    private Strings _strings;
    private int _currentChoice;
    private int _amountOfOptions;
    public string Username = null;
    public string Password = null;
    private ICommand _currentCommand = null;
    private IState _loginState;
    private IState _previousState;
    private IState _currentMenuState;
    private List<IState> _stateHistory;
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

    public IState CurrentState
    {
        get => _currentMenuState;
        set => _currentMenuState = value;
    }

    public IState PreviousState
    {
        get => _previousState;
        set => _previousState = value;
    }

    public Dictionary<string, ICommand> Commands => _commands;

    public Dictionary<StatesEnum, IMenuState> States
    {
        get => _menuStates;
        set => _menuStates = value;
    }

    public List<IState> StateHistory
    {
        get => _stateHistory;
        set => _stateHistory = value;
    }

    public Dictionary<StatesEnum, ILoginState> LoginStates
    {
        get => _loginStates;
        set => _loginStates = value;
    }
    public IState LoginState
    {
        get => _loginState;
        set => _loginState = value;
    }

    /// <summary>
    /// Custom constructor.
    /// </summary>
    /// <param name="webShop"></param>
    /// <param name="commands"></param>
    /// <param name="options"></param>
    /// <param name="quitCommands">Enter one och more quit commands.</param>
    /// <param name="startMenuState"></param>
    /// <param name="startLoginState"></param>
    /// <param name="menuStates"></param>
    /// <param name="loginStates"></param>
    public WebShopMenu(WebShop webShop, Dictionary<string, ICommand> commands, List<string> options, string[] quitCommands, IMenuState startMenuState, ILoginState startLoginState, Dictionary<StatesEnum, IMenuState> menuStates, Dictionary<StatesEnum, ILoginState> loginStates)
    {
        _webShop = webShop;
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
    public WebShopMenu()
    {
        _webShop = new DefaultWebShop();
        _strings = new Strings();
        _commands = new Dictionary<string, ICommand>();
        _options = new List<string>();
        CreateWebShop(defaultConstructor: true);

        CreateDefaultOptions();
        CreateDefaultCommands();
        CreateDefaultStates();
    }

    private void CreateDefaultStates()
    {
        _loginStates = new Dictionary<StatesEnum, ILoginState>();
        _loginStates.Add(StatesEnum.LoggedIn, new LoggedInState(_webShop, this));
        _loginStates.Add(StatesEnum.LoggedOut, new LoggedOutState(this));
        
        _menuStates = new Dictionary<StatesEnum, IMenuState>();
        _menuStates.Add(StatesEnum.CustomerMenu, new CustomerInfoMenuState(this, _webShop));
        _menuStates.Add(StatesEnum.LoginMenu, new LoginMenuState(this, _webShop));
        _menuStates.Add(StatesEnum.MainMenu, new MainMenuState(this, _webShop));
        _menuStates.Add(StatesEnum.PurchaseMenu, new PurchaseMenuState(this, _webShop));
        _menuStates.Add(StatesEnum.SortMenu, new SortMenuState(this, _webShop));
        _menuStates.Add(StatesEnum.WaresMenu, new WaresMenuState(this, _webShop));
        
        _currentMenuState = _menuStates[StatesEnum.MainMenu];
        _loginState = _loginStates[StatesEnum.LoggedOut];
    }

    private void CreateDefaultOptions()
    {
        _options.Add(Strings.Main.Option1);
        _options.Add(Strings.Main.Option2);
        _options.Add(Strings.Main.Option3);
        _options.Add(Strings.Main.Option4);
    }

    private void CreateDefaultCommands()
    {
        _quitCommands = Strings.Quit;

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
    public WebShopMenu(bool loggedInCustomer) : this()
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
            _loginState = _loginStates[StatesEnum.LoggedIn];
            Options[2] = _strings.LogoutString;
        }
    }
    
    private void CreateWebShop(bool defaultConstructor)
    {
        AmountOfOptions = defaultConstructor ? 3 : _options.Count;
        CurrentCustomer = _webShop.CurrentCustomer;
        CurrentChoice = 1;
        PreviousState = _currentMenuState;
        _stateHistory = new List<IState>();
    }

    public void Run()
    {
        Console.WriteLine(Strings.MainMenuWelcome);
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
        if (CurrentState is IMenuState menuState)
        {
            menuState.DisplayOptions();
        }
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