using WebShopCleanCode.Interfaces;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode;

public class WebShopMenu : IMenu
{
    private readonly Dictionary<string, ICommand> _commands;
    private Dictionary<string, IMenu> _menus;
    private List<string> _options;
    private string _quitCommand;
    private WebShop _webShop;
    private Strings _strings;
    private int _currentChoice;
    private int _amountOfOptions;
    public string Username = null;
    public string Password = null;
    private ICommand _currentCommand = null;
    private string _previousMenu = "";
    private IMenuState _previousMenuState;
    private string _currentMenu = "";
    private IMenuState _currentState;
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

    public List<string> Options
    {
        get => _options;
        set => _options = value;
    }

    public Customer CurrentCustomer
    {
        get => _webShop.currentCustomer;
        set => _webShop.currentCustomer = value;
    }

    public Strings Strings
    {
        get => _strings;
        set => _strings = value;
    }

    public Dictionary<string, IMenu> Menus
    {
        get => _menus;
        set => _menus = value;
    }

    public ICommand CurrentCommand
    {
        get => _currentCommand;
        set => _currentCommand = value;
    }

    public string PreviousMenu
    {
        get => _previousMenu;
        set => _previousMenu = value;
    }

    public string CurrentMenu
    {
        get => _currentMenu;
        set => _currentMenu = value;
    }

    public IMenuState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    public IMenuState PreviousMenuState
    {
        get => _previousMenuState;
        set => _previousMenuState = value;
    }

    public Dictionary<string, ICommand> Commands => _commands;

    /// <summary>
    /// Custom constructor.
    /// </summary>
    /// <param name="webShop"></param>
    /// <param name="commands"></param>
    /// <param name="options"></param>
    /// <param name="menus"></param>
    /// <param name="quitCommand"></param>
    /// <param name="startMenu"></param>
    public WebShopMenu(WebShop webShop, Dictionary<string, ICommand> commands, List<string> options, Dictionary<string, IMenu> menus, string quitCommand, string startMenu)
    {
        _webShop = webShop;
        _commands = commands;
        _options = options;
        _menus = menus;
        _quitCommand = quitCommand;
        _currentMenu = startMenu;
        CreateWebShop(defaultConstructor: false);
    }
    
    /// <summary>
    /// Default constructor.
    /// </summary>
    public WebShopMenu()
    {
        _webShop = new WebShop();
        _strings = new Strings();
        _commands = new Dictionary<string, ICommand>();
        _options = new List<string>();
        _menus = new Dictionary<string, IMenu>();
        _currentMenu = _strings.MainMenu;
        CreateWebShop(defaultConstructor: true);

        CreateDefaultOptions();
        CreateDefaultMenus();
        CreateDefaultCommands();
    }

    private void CreateDefaultOptions()
    {
        _options.Add(Strings.Main.Option1);
        _options.Add(Strings.Main.Option2);
        _options.Add(Strings.Main.Option3);
        _options.Add(Strings.Main.Option4);
    }

    private void CreateDefaultMenus()
    {
        Menus.Add("wares menu", new WaresMenu(_webShop, this));
        Menus.Add("main menu", new MainMenu(this, _webShop));
        Menus.Add("purchase menu", new PurchaseMenu(_webShop, this));
    }

    private void CreateDefaultCommands()
    {
        _quitCommand = Strings.Quit;

        Commands.Add("left", new LeftCommand(this));
        Commands.Add("l", new LeftCommand(this));

        Commands.Add("right", new RightCommand(this));
        Commands.Add("r", new RightCommand(this));

        Commands.Add("ok", new OkCommand(_webShop, this));
        Commands.Add("o", new OkCommand(_webShop, this));
        Commands.Add("k", new OkCommand(_webShop, this));

        Commands.Add("quit", new QuitCommand(this));
        Commands.Add("q", new QuitCommand(this));
        
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
            
            _webShop.customers.Add(newCustomer);
            _webShop.currentCustomer = newCustomer;
            _webShop.LoginState = new LoggedInState(_webShop, this);
            Options[2] = "Logout";
        }
    }
    
    private void CreateWebShop(bool defaultConstructor)
    {
        AmountOfOptions = defaultConstructor ? 3 : _options.Count;
        CurrentCustomer = _webShop.currentCustomer;
        CurrentChoice = 1;
        PreviousMenu = _currentMenu;
        _currentState = new MainMenuState(this, _webShop);
        _webShop.LoginState = new LoggedOutState(_webShop, this);
        PreviousMenuState = _currentState;
    }

    public void Run()
    {
        Console.WriteLine(Strings.MainMenuWelcome);
        string input = "";
        
        while (CurrentCommand is not QuitCommand)
        {
            // Menus[_currentMenu].Run();
            // PrintOptions();
            CurrentState.DisplayOptions();
            PrintNavigation();

            input = Console.ReadLine();
            ExecuteCommandIfExists(input);
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