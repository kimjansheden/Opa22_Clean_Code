using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Factories;

public class DefaultWebShopFactory : IWebShopFactory
{

    public WebShop CreateWebShop()
    {
        var webShop = new DefaultWebShop();
        webShop.Initialize();
        return webShop;
    }
}