﻿namespace WebShopCleanCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // WebShopMenu webShopMenu = new WebShopMenu();
            WebShopMenu webShopMenu = new WebShopMenu(loggedInCustomer: true);
            webShopMenu.Run();
            
            // Custom constructor:
            //WebShop webShop = new WebShop();
            //var customMenus = new Dictionary<string, IMenu>
            // {
            //     { "main menu", new MainMenu(webShopMenu) },
            //     { "purchase menu", new PurchaseMenu(webShop, webShopMenu) },
            //     { "wares menu", new WaresMenu(webShop, webShopMenu) }
            // };
            //var webShopMenu = new WebShopMenu(webShop, commands, options, customMenus, quitCommand);
        }
    }
}