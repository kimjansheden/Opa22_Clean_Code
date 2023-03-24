using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class WaresMenu : IMenu
{
    private int _amountOfOptions;
    private List<string> _options;
    private int _currentChoice;
    private readonly WebShop _webShop;
    private readonly WebShopMenu _webShopMenu;
    private Strings _strings;

    public WaresMenu(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;
    }
    public void Run()
    {
        switch (_webShopMenu.CurrentChoice)
            {
                case 1:
                    Console.WriteLine();
                    foreach (Product product in _webShop.products)
                    {
                        product.PrintInfo();
                    }
                    Console.WriteLine();
                    break;
                case 2:
                    if (_webShop.currentCustomer != null)
                    {
                        _strings.currentMenu = "purchase menu";
                        _strings.MainMenuWhat = "What would you like to purchase?";
                        _webShopMenu.CurrentChoice = 1;
                        _webShopMenu.AmountOfOptions = _webShop.products.Count;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("You must be logged in to purchase wares.");
                        Console.WriteLine();
                        _webShopMenu.CurrentChoice = 1;
                    }
                    break;
                case 3:
                    _webShopMenu.Options[0] = "Sort by name, descending";
                    _webShopMenu.Options[1] = "Sort by name, ascending";
                    _webShopMenu.Options[2] = "Sort by price, descending";
                    _webShopMenu.Options[3] = "Sort by price, ascending";
                    _strings.MainMenuWhat = "How would you like to sort them?";
                    _strings.currentMenu = "sort menu";
                    _webShopMenu.CurrentChoice = 1;
                    _webShopMenu.AmountOfOptions = 4;
                    break;
                case 4:
                    if (_webShop.currentCustomer == null)
                    {
                        _webShopMenu.Options[0] = "Set Username";
                        _webShopMenu.Options[1] = "Set Password";
                        _webShopMenu.Options[2] = "Login";
                        _webShopMenu.Options[3] = "Register";
                        _webShopMenu.AmountOfOptions = 4;
                        _strings.MainMenuWhat = "Please submit username and password.";
                        _webShopMenu.CurrentChoice = 1;
                        _strings.currentMenu = "login menu";
                    }
                    else
                    {
                        _webShopMenu.Options[3] = "Login";
                        Console.WriteLine();
                        Console.WriteLine(_webShop.currentCustomer.Username + " logged out.");
                        Console.WriteLine();
                        _webShop.currentCustomer = null;
                        _webShopMenu.CurrentChoice = 1;
                    }
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Not an option.");
                    Console.WriteLine();
                    break;
            }
    }
}