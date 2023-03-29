using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode;

public class Strings
{
    public MainStrings Main = new MainStrings();
    public WaresStrings Wares = new WaresStrings();
    public LoginStrings Login = new LoginStrings();
    public CustomerInfoStrings Customer = new CustomerInfoStrings();
    public SortStrings Sort = new SortStrings();

    public class MainStrings
    {
        public string Option1 = "See Wares";
        public string Option2 = "Customer Info";
        public string Option3 = "Login";
        public string Option4 = "";
    }

    public class SortStrings
    {
        public string Option1 = "Sort by name, descending";
        public string Option2 = "Sort by name, ascending";
        public string Option3 = "Sort by price, descending";
        public string Option4 = "Sort by price, ascending";
        public string How = "How would you like to sort them?";
    }

    public class CustomerInfoStrings
    {
        public string Option1 = "See your orders";
        public string Option2 = "See your info";
        public string Option3 = "Add funds";
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
        public string Menu = "Please submit username and password.";
        public string WriteUsername = "Please write your username.";
        public string UsernameExists = "Username already exists.";
        public string AKeyBoard = "A keyboard appears.";
    }

    
    public string Quit = "quit";
    public string MenuWhat = "What would you like to do?";
    public string MainMenuWelcome = "Welcome to the WebShop!";
    public string LoginString = "Login";
    public string LogoutString = "Logout";
}