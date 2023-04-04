using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.Interfaces;

// In order to avoid calling virtual members in the State constructors, I have implemented the Factory Method Design Pattern.
public interface IMenuStateFactory
{
    MenuState CreateState(App app, WebShop webShop);
}