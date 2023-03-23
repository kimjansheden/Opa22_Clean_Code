using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class WebShopMenu
{
    private readonly Dictionary<string, ICommand> _commands;
    private List<string> _options;
    private string _quitCommand;
    private WebShop _webShop;
    private Strings _strings;
    private int _currentChoice;
    private int _amountOfOptions;
    public string Username = null;
    public string Password = null;
    
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

    public WebShopMenu(WebShop webShop, Dictionary<string, ICommand> commands, List<string> options, string quitCommand)
    {
        _webShop = webShop;
        CreateWebShop();
        _commands = commands;
        _options = options;
        _quitCommand = quitCommand;
    }
    
    public WebShopMenu()
    {
        _webShop = new WebShop();
        CreateWebShop();
        _strings = new Strings();
        _commands = new Dictionary<string, ICommand>();
        _options = new List<string>();
        Options.Add(_strings.Option1);
        Options.Add(_strings.Option2);
        Options.Add(_strings.Option3);
        Options.Add(_strings.Option4);
        _quitCommand = _strings.Quit;
        _commands.Add("left", new LeftCommand(this));
        _commands.Add("l", new LeftCommand(this));
        
        //_commands.Add(ConsoleKey.LeftArrow.ToString(), new LeftCommand(_webShop));
        
        _commands.Add("right", new RightCommand(this));
        _commands.Add("r", new RightCommand(this));
        
        _commands.Add("ok", new OkCommand(_webShop, this));
        _commands.Add("o", new OkCommand(_webShop, this));
        _commands.Add("k", new OkCommand(_webShop, this));
    }
    
    private void CreateWebShop()
    {
        CurrentCustomer = _webShop.currentCustomer;
        CurrentChoice = 1;
        AmountOfOptions = 3;
    }

    public void WebShopMainMenu()
    {
        Console.WriteLine(_strings.MainMenuWelcome);
        string input = "";
        while (!input.Equals(_strings.Quit))
        {
            Console.WriteLine(_strings.MainMenuWhat);
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
        }
        else
        {
            Console.WriteLine("There was no such command.");
        }
    }
}