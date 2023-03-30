using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

public class PurchaseMenuState : MenuState
{
    public PurchaseMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _strings = webShopMenu.Strings;
    }

    protected internal override void DisplayOptions()
    {
        ((ILoginState)_webShopMenu.LoginState).RequestHandle();
    }

    protected internal override void ExecuteOption(int option)
    {
        int index = CurrentChoice - 1;
        Product product = _webShop.Products[index];
        if (product.InStock())
        {
            AttemptPurchase(product);
        }
        else
        {
            PrintMessageWithPadding(_strings.Purchase.NotInStock);
        }
    }

    private void AttemptPurchase(Product product)
    {
        if (_webShop.CurrentCustomer.CanAfford(product.Price))
        {
            CompletePurchase(product);
        }
        else
        {
            PrintMessageWithPadding(_strings.Purchase.CannotAfford);
        }
    }

    private void CompletePurchase(Product product)
    {
        _webShop.CurrentCustomer.Funds -= product.Price;
        product.NrInStock--;
        _webShop.CurrentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
        PrintMessageWithPadding(_strings.Purchase.Success + product.Name);
    }
}