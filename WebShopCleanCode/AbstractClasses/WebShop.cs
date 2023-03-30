namespace WebShopCleanCode.AbstractClasses;

public abstract class WebShop
{
    public Customer CurrentCustomer { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Product> Products { get; set; }
    public abstract void Sort(string variable, bool ascending);
}