namespace WebShopCleanCode.Interfaces;

public interface ICustomerPrinter
{
    void PrintInfo();
    void PrintOrders(List<Order> orders);
    void PrintOtherStuff();
}