using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode;

public class DefaultStrings : Strings
{
    public MainStrings Main = new MainStrings();
    public WaresStrings Wares = new WaresStrings();
    public LoginStrings Login = new LoginStrings();
    public CustomerInfoStrings Customer = new CustomerInfoStrings();
    public SortStrings Sort = new SortStrings();
    public PurchaseStrings Purchase = new PurchaseStrings();

    public class PurchaseStrings
    {
        public string WhatPurchase => "What would you like to purchase?";
        public string CannotAfford => "You cannot afford.";
        public string NotInStock => "Not in stock.";
        public string Success => "Successfully bought ";
    }
    public class MainStrings
    {
        public string Option1 => "See Wares";
        public string Option2 => "Customer Info";
        public string Option3 => "Login";
        public string Option4 => "";
    }

    public class SortStrings
    {
        public string Option1 => "Sort by name, descending";
        public string Option2 => "Sort by name, ascending";
        public string Option3 => "Sort by price, descending";
        public string Option4 => "Sort by price, ascending";
        public string How => "How would you like to sort them?";
        public string WaresSorted => "Wares sorted.";
    }

    public class CustomerInfoStrings
    {
        public string Option1 => "See your orders";
        public string Option2 => "See your info";
        public string Option3 => "Add funds";
        public string DontAddNegative => "Don't add negative amounts.";
        public string Added => " added to your profile.";
        public string PleaseWriteNum => "Please write a number next time.";
        public string NumTooHigh => "The number was too high. Please try a lower number.";
        public string HowManyFunds => "How many funds would you like to add?";
    }

    public class WaresStrings
    {
        public string Option1 => "See all wares";
        public string Option2 => "Purchase a ware";
        public string Option3 => "Sort wares";
    }
    public class LoginStrings
    {
        public string AKeyBoard => "A keyboard appears.";
        public string WantFirstName => "Do you want a first name? y/n";
        public string WantLastName => "Do you want a last name? y/n";
        public string WantPassword => "Do you want a password? y/n";
        public string WantPhone => "Do you want a phone number? y/n";
        public string WantAddress => "Do you want an address? y/n";
        public string WantAge => "Do you want an age? y/n";
        public string WantEmail => "Do you want an email? y/n";
        public string IncompleteData => "Incomplete data.";
        public string InvalidCreds => "Invalid credentials.";
        public string Option3 => "Login";
        public string NobodyLoggedIn => "Nobody is logged in.";
        public string WriteSomething => "Please actually write something.";
        public string InputPassword => "Please input your password.";
        public string InputUsername => "Please input your username.";
        public string Menu => "Please submit username and password.";
        public string WriteNumber => "Please write a number.";
        public string WriteAddress => "Please write your address.";
        public string WriteAge => "Please write your age.";
        public string WriteEmail => "Please write your email.";
        public string WriteFirstName => "Please write your first name.";
        public string WriteLastName => "Please write your last name.";
        public string WritePassword => "Please write your password.";
        public string WritePhone => "Please write your phone number.";
        public string WriteUsername => "Please write your username.";
        public string Option4 => "Register";
        public string Option2 => "Set Password";
        public string Option1 => "Set Username";
        public string UsernameExists => "Username already exists.";
        public string MustBeLoggedIn => "You must be logged in to purchase wares.";
        public string YOrN => "y or n, please.";
    }
    
    public string[] Quit => new[] { "quit", "q" };
    public string MenuWhat => "What would you like to do?";
}