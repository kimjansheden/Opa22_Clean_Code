using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.MenuStates;

namespace WebShopCleanCode.Factories;

public class SortMenuStateFactory : IMenuStateFactory
{
    public MenuState CreateState(App app, WebShop webShop)
    {
        var state = new SortMenuState(app, webShop);
        state.Initialize();
        return state;
    }
}