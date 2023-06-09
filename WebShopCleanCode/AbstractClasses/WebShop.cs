namespace WebShopCleanCode.AbstractClasses;

public abstract class WebShop
{
    public virtual Customer CurrentCustomer { get; internal set; }
    public virtual List<Customer> Customers { get; protected set; }
    public virtual List<Product> Products { get; protected set; }
    public abstract void Sort(Enum sortBy, bool order);
    public abstract void Initialize();
}