using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Factories;
using WebShopCleanCode.Helpers;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // IApp app = new App();
            // app.Run();
            
             //Custom constructor:
             IWebShopFactory webShopFactory = new DefaultWebShopFactory();
             IMenuManager menuManager = new MenuManager();
             IOptionsManager optionsManager = new OptionsManager();
             ICommandExecutor commandExecutor = new CommandExecutor();
             Strings strings = new DefaultStrings();
             
             var menuStateFactories = new Dictionary<string, IMenuStateFactory>();
             menuStateFactories.Add("CustomerMenu", new CustomerInfoMenuMenuStateFactory());
             menuStateFactories.Add("LoginMenu", new LoginMenuStateFactory());
             menuStateFactories.Add("MainMenu", new MainMenuStateFactory());
             menuStateFactories.Add("PurchaseMenu", new PurchaseMenuStateFactory());
             menuStateFactories.Add("SortMenu", new SortMenuStateFactory());
             menuStateFactories.Add("WaresMenu", new WaresMenuStateFactory());
             
             var loginStateFactories = new Dictionary<string, ILoginStateFactory>();
             loginStateFactories.Add("LoggedIn", new LoggedInStateFactory());
             loginStateFactories.Add("LoggedOut", new LoggedOutStateFactory());

             IApp app = new App(webShopFactory, menuManager, optionsManager, commandExecutor, strings, menuStateFactories, loginStateFactories, );
        }
    }
}