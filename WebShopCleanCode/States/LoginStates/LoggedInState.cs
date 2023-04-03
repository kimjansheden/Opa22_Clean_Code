using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.States.MenuStates;

namespace WebShopCleanCode.States.LoginStates;

public class LoggedInState : LoginState
{
    public LoggedInState(WebShop webShop, App app) : base(app, webShop)
    {
        
    }

    protected internal override void RequestHandle(State state)
    {
        if (state is PurchaseMenuState)
        {
            PurchaseMenuHandle();
        }
        else if (state is CustomerInfoMenuState)
        {
            CustomerInfoMenuHandle(state);
        }
    }

    private void PurchaseMenuHandle()
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
        LoginState = App.LoginStates["LoggedOut"];
    }
    
    private void CustomerInfoMenuHandle(State state)
    {
        App.SetOptions(state.Options);
        AmountOfOptions = 3;
        Console.WriteLine(((DefaultStrings)Strings).MenuWhat);
        App.PrintOptions();
    }
}