namespace WebShopCleanCode.AbstractClasses;

public abstract class WebShop
{
    public Customer CurrentCustomer { get; internal set; }
    public List<Customer> Customers { get; protected init; }
    public List<Product> Products { get; protected init; }
    public abstract void Sort(string variable, bool ascending);
}