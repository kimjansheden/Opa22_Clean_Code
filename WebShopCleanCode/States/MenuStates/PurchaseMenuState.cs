using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.MenuStates;

public class PurchaseMenuState : MenuState
{
    public PurchaseMenuState(App app, WebShop webShop)
    {
        App = app;
        WebShop = webShop;
    }

    protected internal override void DisplayOptions()
    {
        ((LoginState)App.LoginState).RequestHandle();
    }

    protected internal override void ExecuteOption(int option)
    {
        int index = CurrentChoice - 1;
        Product product = WebShop.Products[index];
        if (product.InStock())
        {
            AttemptPurchase(product);
        }
        else
        {
            PrintMessageWithPadding(((DefaultStrings)Strings).Purchase.NotInStock);
        }
    }

    private void AttemptPurchase(Product product)
    {
        if (CurrentCustomer.CanAfford(product.Price))
        {
            CompletePurchase(product);
        }
        else
        {
            PrintMessageWithPadding(((DefaultStrings)Strings).Purchase.CannotAfford);
        }
    }

    private void CompletePurchase(Product product)
    {
        CurrentCustomer.Funds -= product.Price;
        product.NrInStock--;
        CurrentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
        PrintMessageWithPadding(((DefaultStrings)Strings).Purchase.Success + product.Name);
    }
}