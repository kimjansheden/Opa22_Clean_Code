using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Helpers;

public class PurchaseHelper : IPurchaseHelper
{
    private readonly Strings _strings;
    public PurchaseHelper(Strings strings)
    {
        _strings = strings;
    }

    public void AttemptPurchase(Product product, Customer currentCustomer, Action<string> printMessageWithPadding)
    {
        if (product.InStock())
        {
            if (currentCustomer.CanAfford(product.ProductPrice))
            {
                CompletePurchase(product, currentCustomer);
                printMessageWithPadding(((DefaultStrings)_strings).Purchase.Success + product.ProductName);
            }
            else
            {
                printMessageWithPadding(((DefaultStrings)_strings).Purchase.CannotAfford);
            }
        }
        else
        {
            printMessageWithPadding(((DefaultStrings)_strings).Purchase.NotInStock);
        }
    }
    
    private void CompletePurchase(Product product, Customer currentCustomer)
    {
        var currentFunds = currentCustomer.Info.GetInfo<int>("Funds");
        var newFunds = currentFunds - product.ProductPrice;
        currentCustomer.Info.SetInfo<int>("Funds", newFunds);
        product.NrInStock--;
        currentCustomer.Orders.Add(new Order(product.ProductName, product.ProductPrice, DateTime.Now));
    }
}