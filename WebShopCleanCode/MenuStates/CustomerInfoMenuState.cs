using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.LoginStates;

namespace WebShopCleanCode.MenuStates;
public class CustomerInfoMenuState : MenuState
{
    public CustomerInfoMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _strings = webShopMenu.Strings;
        _optionActions = new Dictionary<int, Action>
        {
            { 1, SeeOrders },
            { 2, SeeInfo },
            { 3, AddFunds }
        };
        _options = new List<string>
        {
            _webShopMenu.Strings.Customer.Option1,
            _webShopMenu.Strings.Customer.Option2,
            _webShopMenu.Strings.Customer.Option3,
        };
        webShopMenu.CurrentChoice = 1;
    }
    private void AddFunds()
    {
        Console.WriteLine("How many funds would you like to add?");
        string amountString = Console.ReadLine();
        try
        {
            int amount = int.Parse(amountString);
            if (amount < 0)
            {
                Console.WriteLine();
                Console.WriteLine("Don't add negative amounts.");
                Console.WriteLine();
            }
            else
            {
                _webShop.CurrentCustomer.Funds += amount;
                Console.WriteLine();
                Console.WriteLine(amount + " added to your profile.");
                Console.WriteLine();
            }
        }
        catch (FormatException e)
        {
            Console.WriteLine();
            Console.WriteLine("Please write a number next time.");
            Console.WriteLine();
        }
    }

    private void SeeInfo()
    {
        _webShop.CurrentCustomer.PrintInfo();
    }

    private void SeeOrders()
    {
        _webShop.CurrentCustomer.PrintOrders();
    }

    protected internal override void DisplayOptions()
    {
        if (_webShopMenu.LoginState is LoggedOutState)
        {
            PrintMessageWithPadding(_strings.Login.NobodyLoggedIn);
        }
        else if (_webShopMenu.LoginState is LoggedInState)
        {
            _webShopMenu.SetOptions(_options);
            _webShopMenu.AmountOfOptions = 3;
            Console.WriteLine(_strings.MenuWhat);
            _webShopMenu.PrintOptions();
        }
    }
}