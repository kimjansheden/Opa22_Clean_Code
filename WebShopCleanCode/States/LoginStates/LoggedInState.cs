using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.LoginStates;

public class LoggedInState : LoginState
{
    public LoggedInState(WebShop webShop, App app)
    {
        WebShop = webShop;
        App = app;
    }

    protected internal override void RequestHandle()
    {
        DisplayProductsAndFunds();
        App.ClearAllOptions();
    }

    private void DisplayProductsAndFunds()
    {
        Console.WriteLine(((DefaultStrings)Strings).Purchase.WhatPurchase);
        AmountOfOptions = WebShop.Products.Count;
        for (int i = 0; i < AmountOfOptions; i++)
        {
            Console.WriteLine(i + 1 + ": " + WebShop.Products[i].Name + ", " + WebShop.Products[i].Price + "kr");
        }
        Console.WriteLine("Your funds: " + CurrentCustomer.Funds);
    }

    protected internal override void LoginLogoutHandle()
    {
        PrintMessageWithPadding(CurrentCustomer.Username + " logged out.");
        CurrentCustomer = null;
        CurrentChoice = 1;
        App.LoginState = App.LoginStates["LoggedOut"];
    }
}