using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class Strings
{
    public MainStrings Main = new MainStrings();
    public WaresStrings Wares = new WaresStrings();
    public LoginStrings Login = new LoginStrings();

    public class MainStrings
    {
        public string Option1 = "See Wares";
        public string Option2 = "Customer Info";
        public string Option3 = "Login";
        public string Option4 = "";
    }

    public class WaresStrings
    {
        public string Option1 = "See all wares";
        public string Option2  = "Purchase a ware";
        public string Option3  = "Sort wares";
    }
    public class LoginStrings
    {
        public string Option1 = "Set Username";
        public string Option2  = "Set Password";
        public string Option3  = "Login";
        public string Option4  = "Register";
    }

    
    public string Quit = "quit";
    public string WaresMenu = "wares menu";
    public string PurchaseMenu = "purchase menu";
    public string MainMenu = "main menu";
    public string MainMenuWhat = "What would you like to do?";
    public string MainMenuWelcome = "Welcome to the WebShop!";
    public string LoginString = "Login";
    public string LogoutString = "Logout";
    private Tuple<string> _strings;

    public Strings()
    {
        
    }
}