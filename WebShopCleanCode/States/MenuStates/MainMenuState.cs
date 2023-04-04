using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.MenuStates;

public class MainMenuState : MenuState
{
    protected override string DisplayMessage => ((DefaultStrings)Strings).MenuWhat;

    public MainMenuState(App app, WebShop webShop) : base(app, webShop)
    {
        
    }
    
    protected internal override void Initialize()
    {
        OptionActions = new Dictionary<int, Action>
        {
            { 1, ShowWaresMenu },
            { 2, ShowCustomerInfo },
            { 3, LoginOrLogout }
        };
        Options = new List<string>
        {
            ((DefaultStrings)Strings).Main.Option1,
            ((DefaultStrings)Strings).Main.Option2,
            LoginStateString
        };
        CurrentChoice = 1;
    }

    private void LoginOrLogout()
    {
        LoginState.LoginLogoutHandle();
    }

    private void ShowCustomerInfo()
    {
        ChangeState("CustomerMenu");
    }

    protected internal override void DisplayOptions()
    {
        SetLoginState();
        base.DisplayOptions();
    }

    private void ShowWaresMenu()
    {
        ChangeState("WaresMenu");
    }
}