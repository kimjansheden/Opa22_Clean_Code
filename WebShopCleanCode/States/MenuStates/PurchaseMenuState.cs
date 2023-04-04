using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Helpers;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.States.MenuStates;

public class PurchaseMenuState : MenuState
{
    private IPurchaseHelper _purchaseHelper = null!; 
    public PurchaseMenuState(App app, WebShop webShop) : base(app, webShop)
    {
    }
    
    protected internal override void Initialize()
    {
        _purchaseHelper = new PurchaseHelper(Strings);
    }

    protected internal override void DisplayOptions()
    {
        LoginState.RequestHandle(this);
    }

    protected internal override void ExecuteOption(int option)
    {
        int index = CurrentChoice - 1;
        Product product = WebShop.Products[index];
        _purchaseHelper.AttemptPurchase(product, CurrentCustomer, PrintMessageWithPadding);
    }
}