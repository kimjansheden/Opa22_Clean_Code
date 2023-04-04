namespace WebShopCleanCode.AbstractClasses;

public abstract class Strings
{
    public virtual string MainMenuWelcome => "Welcome to the WebShop!";
    public virtual string LoginString => "Login";
    public virtual string LogoutString => "Logout";
    public virtual string PowerDown => "The console powers down. You are free to leave.";
    public virtual string Buttons => "Your buttons are Left, Right, OK, Back and Quit.";
    public virtual string NotApplicable => "That is not an applicable option.";
}