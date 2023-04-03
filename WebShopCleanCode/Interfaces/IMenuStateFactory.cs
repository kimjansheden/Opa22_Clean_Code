using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.Interfaces;

public interface IMenuStateFactory
{
    MenuState CreateState(App app, WebShop webShop);
}