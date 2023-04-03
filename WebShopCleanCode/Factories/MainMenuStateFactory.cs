using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;
using WebShopCleanCode.States.MenuStates;

namespace WebShopCleanCode.Factories;

public class MainMenuStateFactory : IMenuStateFactory
{
    public MenuState CreateState(App app, WebShop webShop)
    {
        var state = new MainMenuState(app, webShop);
        state.Initialize();
        return state;
    }
}