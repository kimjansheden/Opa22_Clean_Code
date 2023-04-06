using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.Interfaces;

// In order to avoid calling virtual members in the constructors, I have implemented the Factory Method Design Pattern.
public interface IWebShopFactory
{
    WebShop CreateWebShop();
}