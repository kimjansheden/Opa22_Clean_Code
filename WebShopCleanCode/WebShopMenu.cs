using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class WebShopMenu
{
    private readonly Dictionary<string, ICommand> _commands;
    private readonly List<string> _options;
    private string _quitCommand;
    private WebShop _webShop;
    private Strings _strings;
    private int _amountOfOptions;
    private Customer _currentCustomer;
    
    public int CurrentChoice
    {
        get => _webShop.currentChoice;
        set => _webShop.currentChoice = value;
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
        _options.Add(_strings.Option1);
        _options.Add(_strings.Option2);
        _options.Add(_strings.Option3);
        _options.Add(_strings.Option4);
        _options.Add(_strings.Info);
        _quitCommand = _strings.Quit;
        _commands.Add("left", new LeftCommand(_webShop));
        _commands.Add("right", new RightCommand(_webShop));
    }

    

    private void CreateWebShop()
    {
        _currentCustomer = _webShop.currentCustomer;
        CurrentChoice = _webShop.currentChoice;
        _amountOfOptions = _webShop.amountOfOptions;
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
        foreach (string option in _options)
        {
            Console.WriteLine(option);
        }
    }

    private void PrintNavigation()
    {
        for (int i = 0; i < _amountOfOptions; i++)
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
        DisplayUser(_currentCustomer);

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