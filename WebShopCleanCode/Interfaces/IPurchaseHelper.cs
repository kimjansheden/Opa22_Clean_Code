namespace WebShopCleanCode.Interfaces;

public interface IPurchaseHelper
{
    void AttemptPurchase(Product product, 
        Customer currentCustomer, 
        Action<string> printMessageWithPadding);
}