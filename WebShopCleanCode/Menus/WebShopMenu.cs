using WebShopCleanCode.Interfaces;

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

    /// <summary>
    /// Custom constructor.
    /// </summary>
    /// <param name="webShop"></param>
    /// <param name="commands"></param>
    /// <param name="options"></param>
    /// <param name="menus"></param>
    /// <param name="quitCommand"></param>
    public WebShopMenu(WebShop webShop, Dictionary<string, ICommand> commands, List<string> options, Dictionary<string, IMenu> menus, string quitCommand)
    {
        _webShop = webShop;
        _commands = commands;
        _options = options;
        Menus = menus;
        CreateWebShop(defaultConstructor: false);
        _quitCommand = quitCommand;
    }
    
    /// <summary>
    /// Default constructor.
    /// </summary>
    public WebShopMenu()
    {
        _webShop = new WebShop();
        _strings = new Strings();
        CreateWebShop(defaultConstructor: true);
        _commands = new Dictionary<string, ICommand>();
        _options = new List<string>();
        Menus = new Dictionary<string, IMenu>();

        CreateDefaultOptions();
        CreateDefaultMenus();
        CreateDefaultCommands();
    }

    private void CreateDefaultOptions()
    {
        _options.Add(Strings.Option1);
        _options.Add(Strings.Option2);
        _options.Add(Strings.Option3);
        _options.Add(Strings.Option4);
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

        _commands.Add("left", new LeftCommand(this));
        _commands.Add("l", new LeftCommand(this));

        _commands.Add("right", new RightCommand(this));
        _commands.Add("r", new RightCommand(this));

        _commands.Add("ok", new OkCommand(_webShop, this));
        _commands.Add("o", new OkCommand(_webShop, this));
        _commands.Add("k", new OkCommand(_webShop, this));

        _commands.Add("quit", new QuitCommand(this));
        _commands.Add("q", new QuitCommand(this));
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
            Options[2] = "Logout";
        }
    }
    
    private void CreateWebShop(bool defaultConstructor)
    {
        AmountOfOptions = defaultConstructor ? 3 : _options.Count;
        CurrentCustomer = _webShop.currentCustomer;
        CurrentChoice = 1;
    }

    public void Run()
    {
        Console.WriteLine(Strings.MainMenuWelcome);
        string input = "";
        
        while (_currentCommand is not QuitCommand)
        {
            Menus[_strings.currentMenu].Run();
            PrintOptions();
            PrintNavigation();

            input = Console.ReadLine();
            ExecuteCommandIfExists(input);
        }
    }
    private void PrintOptions()
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
        if (_commands.ContainsKey(input))
        {
            _commands[input].Execute();
            _currentCommand = _commands[input];
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
}