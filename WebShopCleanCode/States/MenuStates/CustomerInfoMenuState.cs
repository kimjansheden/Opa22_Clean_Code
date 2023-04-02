using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.States.LoginStates;

namespace WebShopCleanCode.States.MenuStates;
public class CustomerInfoMenuState : MenuState
{
    public CustomerInfoMenuState(App app, WebShop webShop)
    {
        App = app;
        WebShop = webShop;
        OptionActions = new Dictionary<int, Action>
        {
            { 1, SeeOrders },
            { 2, SeeInfo },
            { 3, AddFunds }
        };
        Options = new List<string>
        {
            ((DefaultStrings)Strings).Customer.Option1,
            ((DefaultStrings)Strings).Customer.Option2,
            ((DefaultStrings)Strings).Customer.Option3,
        };
        app.CurrentChoice = 1;
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
                CurrentCustomer.Funds += amount;
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
        CurrentCustomer.PrintInfo();
    }

    private void SeeOrders()
    {
        CurrentCustomer.PrintOrders();
    }

    protected internal override void DisplayOptions()
    {
        if (App.LoginState is LoggedOutState)
        {
            PrintMessageWithPadding(((DefaultStrings)Strings).Login.NobodyLoggedIn);
            App.Commands["back"].Execute();
            App.DisplayOptions();
        }
        else if (App.LoginState is LoggedInState)
        {
            App.SetOptions(Options);
            AmountOfOptions = 3;
            Console.WriteLine(((DefaultStrings)Strings).MenuWhat);
            App.PrintOptions();
        }
    }
}