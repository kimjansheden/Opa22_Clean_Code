using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.MenuStates;

namespace WebShopCleanCode.Factories;

public class PurchaseMenuStateFactory : IMenuStateFactory
{
    public MenuState CreateState(App app, WebShop webShop)
    {
        var state = new PurchaseMenuState(app, webShop);
        state.Initialize();
        return state;
    }
}