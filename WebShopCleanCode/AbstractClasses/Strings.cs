namespace WebShopCleanCode.AbstractClasses;

public abstract class Strings
{
    public virtual string MainMenuWelcome { get; set; } = "Welcome to the WebShop!";
    public virtual string LoginString { get; set; } = "Login";
    public virtual string LogoutString { get; set; } = "Logout";
}