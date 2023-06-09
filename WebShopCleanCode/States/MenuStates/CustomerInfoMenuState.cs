using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.MenuStates;
public class CustomerInfoMenuState : MenuState
{
    public CustomerInfoMenuState(App app, WebShop webShop) : base(app, webShop)
    {
    }
    protected internal override void Initialize()
    {
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
        CurrentChoice = 1;
    }
    protected internal override void DisplayOptions()
    {
        LoginState.RequestHandle(this);
    }
    private void AddFunds()
    {
        Console.WriteLine(((DefaultStrings)Strings).Customer.HowManyFunds);
        string amountString = Console.ReadLine();
        try
        {
            int amount = int.Parse(amountString);
            if (amount < 0)
            {
                PrintMessageWithPadding(((DefaultStrings)Strings).Customer.DontAddNegative);
            }
            else
            {
                var currentFunds = CurrentCustomer.Info.GetInfo<int>("Funds");
                var newFunds = currentFunds + amount;
                CurrentCustomer.Info.SetInfo<int>("Funds", newFunds);
                PrintMessageWithPadding(amount + ((DefaultStrings)Strings).Customer.Added);
                CurrentChoice = 1;
            }
        }
        catch (FormatException e)
        {
            PrintMessageWithPadding(((DefaultStrings)Strings).Customer.PleaseWriteNum);
        }
        catch (OverflowException e)
        {
            PrintMessageWithPadding(((DefaultStrings)Strings).Customer.NumTooHigh);
        }
    }

    private void SeeInfo()
    {
        CurrentCustomer.Printer.PrintInfo();
        CurrentChoice = 1;
    }

    private void SeeOrders()
    {
        CurrentCustomer.Printer.PrintOrders();
    }
}