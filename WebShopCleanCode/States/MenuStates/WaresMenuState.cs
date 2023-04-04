using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.MenuStates;
public class WaresMenuState : MenuState
{
    protected override string DisplayMessage => ((DefaultStrings)Strings).MenuWhat;

    public WaresMenuState(App app, WebShop webShop) : base(app, webShop)
    {
        
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
    
    protected internal override void DisplayOptions()
    {
        SetLoginState();
        base.DisplayOptions();
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
}