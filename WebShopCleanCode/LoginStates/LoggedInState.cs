using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.LoginStates;

public class LoggedInState : ILoginState
{
    private readonly WebShop _webShop;
    private Strings _strings;
    private readonly WebShopMenu _webShopMenu;
    private int AmountOfOptions
    {
        get => _webShopMenu.AmountOfOptions;
        set => _webShopMenu.AmountOfOptions = value;
    }

    private IState CurrentState
    {
        get => _webShopMenu.CurrentState;
        set => _webShopMenu.CurrentState = value;
    }

    private Dictionary<StatesEnum, IMenuState> States
    {
        get => _webShopMenu.States;
        set => _webShopMenu.States = value;
    }

    private int CurrentChoice
    {
        get => _webShopMenu.CurrentChoice;
        set => _webShopMenu.CurrentChoice = value;
    }

    public LoggedInState(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;
    }

    public void RequestHandle()
    {
        Console.WriteLine(_strings.Purchase.WhatPurchase);
        AmountOfOptions = _webShop.Products.Count;
        for (int i = 0; i < AmountOfOptions; i++)
        {
            Console.WriteLine(i + 1 + ": " + _webShop.Products[i].Name + ", " + _webShop.Products[i].Price + "kr");
        }
        Console.WriteLine("Your funds: " + _webShop.CurrentCustomer.Funds);
        _webShopMenu.ClearAllOptions();
    }

    public void LoginOutHandle()
    {
        Console.WriteLine();
        Console.WriteLine(_webShop.CurrentCustomer.Username + " logged out.");
        Console.WriteLine();
        _webShop.CurrentCustomer = null;
        _webShopMenu.LoginState = _webShopMenu.LoginStates[StatesEnum.LoggedOut];
    }

    public void ChangeState(StatesEnum stateEnum)
    {
        // PreviousState = this;
        // StateHistory.Add(this);
        CurrentState = States[stateEnum];
        CurrentChoice = 1;
    }
}