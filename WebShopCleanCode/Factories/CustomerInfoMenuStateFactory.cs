using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.MenuStates;

namespace WebShopCleanCode.Factories;

public class CustomerInfoMenuMenuStateFactory : IMenuStateFactory
{
    public MenuState CreateState(App app, WebShop webShop)
    {
        var state = new CustomerInfoMenuState(app, webShop);
        state.Initialize();
        return state;
    }
}