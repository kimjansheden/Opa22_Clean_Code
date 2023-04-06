using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Commands;
using WebShopCleanCode.Factories;
using WebShopCleanCode.Helpers;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Custom constructor setting. Choose true or false to use your own custom settings or not.
            var useCustomConstructor = false;

            // Default constructor.
            IApp app = new App();
            
            // Custom constructor. Set the bool to true if you want to use
            // this constructor instead of the default one.
            if (useCustomConstructor)
            {
                app = CreateCustomApp();
            }
            
             // Run the app.
             app.Run();
        }

        /// <summary>
        /// Choose your own settings and build your own web shop. Add new classes and expand the app as you'd like.
        /// </summary>
        private static IApp CreateCustomApp()
        {
            IWebShopFactory webShopFactory = new DefaultWebShopFactory();
            IMenuManager menuManager = new MenuManager();
            IOptionsManager optionsManager = new OptionsManager();
            ICommandExecutor commandExecutor = new CommandExecutor();
            Strings strings = new DefaultStrings();

            var menuStateFactories = new Dictionary<string, IMenuStateFactory>();
            menuStateFactories.Add("CustomerMenu", new CustomerInfoMenuStateFactory());
            menuStateFactories.Add("LoginMenu", new LoginMenuStateFactory());
            menuStateFactories.Add("MainMenu", new MainMenuStateFactory());
            menuStateFactories.Add("PurchaseMenu", new PurchaseMenuStateFactory());
            menuStateFactories.Add("SortMenu", new SortMenuStateFactory());
            menuStateFactories.Add("WaresMenu", new WaresMenuStateFactory());

            var loginStateFactories = new Dictionary<string, ILoginStateFactory>();
            loginStateFactories.Add("LoggedIn", new LoggedInStateFactory());
            loginStateFactories.Add("LoggedOut", new LoggedOutStateFactory());

            var quitCommands = ((DefaultStrings)strings).Quit;

            var commands = new Dictionary<string, ICommand>();

            commands.Add("left", new LeftCommand());
            commands.Add("l", new LeftCommand());

            commands.Add("right", new RightCommand());
            commands.Add("r", new RightCommand());

            commands.Add("ok", new OkCommand());
            commands.Add("o", new OkCommand());
            commands.Add("k", new OkCommand());
            commands.Add("okay", new OkCommand());


            commands.Add("back", new BackCommand());
            commands.Add("b", new BackCommand());

            var currentState = "MainMenu";
            var loginState = "LoggedOut";
            
            return new App(webShopFactory, menuManager, optionsManager, commandExecutor, strings, menuStateFactories, loginStateFactories, commands, quitCommands, currentState, loginState);
        }
    }
}