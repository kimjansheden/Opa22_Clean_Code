namespace WebShopCleanCode;

public class Strings
{
    public MainStrings Main = new MainStrings();
    public WaresStrings Wares = new WaresStrings();
    public LoginStrings Login = new LoginStrings();
    public CustomerInfoStrings Customer = new CustomerInfoStrings();
    public SortStrings Sort = new SortStrings();
    public PurchaseStrings Purchase = new PurchaseStrings();

    public class PurchaseStrings
    {
        public string WhatPurchase { get; set; } = "What would you like to purchase?";
        public string CannotAfford { get; set; } = "You cannot afford.";
        public string NotInStock { get; set; } = "Not in stock.";
        public string Success { get; set; } = "Successfully bought ";
    }
    public class MainStrings
    {
        public string Option1 { get; set; } = "See Wares";
        public string Option2 { get; set; } = "Customer Info";
        public string Option3 { get; set; } = "Login";
        public string Option4 { get; set; } = "";
    }

    public class SortStrings
    {
        public string Option1 { get; set; } = "Sort by name, descending";
        public string Option2 { get; set; } = "Sort by name, ascending";
        public string Option3 { get; set; } = "Sort by price, descending";
        public string Option4 { get; set; } = "Sort by price, ascending";
        public string How { get; set; } = "How would you like to sort them?";
        public string WaresSorted { get; set; } = "Wares sorted.";
    }

    public class CustomerInfoStrings
    {
        public string Option1 { get; set; } = "See your orders";
        public string Option2 { get; set; } = "See your info";
        public string Option3 { get; set; } = "Add funds";
    }

    public class WaresStrings
    {
        public string Option1 { get; set; } = "See all wares";
        public string Option2 { get; set; } = "Purchase a ware";
        public string Option3 { get; set; } = "Sort wares";
    }
    public class LoginStrings
    {
        public string AKeyBoard { get; set; } = "A keyboard appears.";
        public string WantFirstName { get; set; } = "Do you want a first name? y/n";
        public string WantLastName { get; set; } = "Do you want a last name? y/n";
        public string WantPhone { get; set; } = "Do you want a phone number? y/n";
        public string WantAddress { get; set; } = "Do you want an address? y/n";
        public string WantAge { get; set; } = "Do you want an age? y/n";
        public string WantEmail { get; set; } = "Do you want an email? y/n";
        public string IncompleteData { get; set; } = "Incomplete data.";
        public string InvalidCreds { get; set; } = "Invalid credentials.";
        public string Option3 { get; set; } = "Login";
        public string NobodyLoggedIn { get; set; } = "Nobody is logged in.";
        public string WriteSomething { get; set; } = "Please actually write something.";
        public string InputPassword { get; set; } = "Please input your password.";
        public string InputUsername { get; set; } = "Please input your username.";
        public string Menu { get; set; } = "Please submit username and password.";
        public string WriteNumber { get; set; } = "Please write a number.";
        public string WriteAddress { get; set; } = "Please write your address.";
        public string WriteAge { get; set; } = "Please write your age.";
        public string WriteEmail { get; set; } = "Please write your email.";
        public string WriteFirstName { get; set; } = "Please write your first name.";
        public string WriteLastName { get; set; } = "Please write your last name.";
        public string WritePhone { get; set; } = "Please write your phone number.";
        public string WriteUsername { get; set; } = "Please write your username.";
        public string Option4 { get; set; } = "Register";
        public string Option2 { get; set; } = "Set Password";
        public string Option1 { get; set; } = "Set Username";
        public string UsernameExists { get; set; } = "Username already exists.";
        public string MustBeLoggedIn { get; set; } = "You must be logged in to purchase wares.";
        public string YOrN { get; set; } = "y or n, please.";
    }

    
    public string[] Quit = new[] { "quit", "q" };
    public string MenuWhat { get; set; } = "What would you like to do?";
    public string MainMenuWelcome { get; set; } = "Welcome to the WebShop!";
    public string LoginString { get; set; } = "Login";
    public string LogoutString { get; set; } = "Logout";
}