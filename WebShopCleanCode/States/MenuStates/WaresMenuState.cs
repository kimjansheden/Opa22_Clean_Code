using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.MenuStates;
public class WaresMenuState : MenuState
{
    public WaresMenuState(App app, WebShop webShop) : base(app, webShop)
    {
        
    }

    private void LoginOrLogout()
    {
        LoginState.LoginLogoutHandle();
    }

    private void SortWares()
    {
        ChangeState("SortMenu");
    }

    private void ShowPurchaseWaresMenu()
    {
        ChangeState("PurchaseMenu");
    }
    
    private void SeeWares()
    {
        Console.WriteLine();
        foreach (Product product in WebShop.Products)
        {
            product.PrintInfo();
        }
        Console.WriteLine();
    }

    protected internal override void DisplayOptions()
    {
        SetLoginState();
        App.SetOptions(Options);
        AmountOfOptions = 4;
        Console.WriteLine(((DefaultStrings)Strings).MenuWhat);
        App.PrintOptions();
    }

    protected internal override void Initialize()
    {
        OptionActions = new Dictionary<int, Action>
        {
            { 1, SeeWares },
            { 2, ShowPurchaseWaresMenu },
            { 3, SortWares },
            {4, LoginOrLogout}
        };
        Options = new List<string>
        {
            ((DefaultStrings)Strings).Wares.Option1,
            ((DefaultStrings)Strings).Wares.Option2,
            ((DefaultStrings)Strings).Wares.Option3,
            LoginStateString
        };
        CurrentChoice = 1;
    }
}