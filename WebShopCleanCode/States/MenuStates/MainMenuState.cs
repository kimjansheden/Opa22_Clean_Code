using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.MenuStates;

public class MainMenuState : MenuState
{
    public MainMenuState(App app, WebShop webShop)
    {
        App = app;
        WebShop = webShop;
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
            LoginState
        };
        app.CurrentChoice = 1;
    }

    private void LoginOrLogout()
    {
        ((LoginState)App.LoginState).LoginLogoutHandle();
    }

    private void ShowCustomerInfo()
    {
        ChangeState("CustomerMenu");
    }

    protected internal override void DisplayOptions()
    {
        SetLoginState();
        App.SetOptions(Options);
        AmountOfOptions = 3;
        Console.WriteLine(((DefaultStrings)Strings).MenuWhat);
        App.PrintOptions();
    }

    private void ShowWaresMenu()
    {
        ChangeState("WaresMenu");
    }
}