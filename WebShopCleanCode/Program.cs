namespace WebShopCleanCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // WebShopMenu webShopMenu = new WebShopMenu();
            WebShopMenu webShopMenu = new WebShopMenu(loggedInCustomer: true);
            webShopMenu.Run();
        }
    }
}