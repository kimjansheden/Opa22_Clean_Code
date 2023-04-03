using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.MenuStates;

namespace WebShopCleanCode.Factories;

public class WaresMenuStateFactory : IMenuStateFactory
{
    public MenuState CreateState(App app, WebShop webShop)
    {
        var state = new WaresMenuState(app, webShop);
        state.Initialize();
        return state;
    }
}