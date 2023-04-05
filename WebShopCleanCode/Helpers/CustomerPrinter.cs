using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Helpers;

public class CustomerPrinter : ICustomerPrinter
{
    private readonly ICustomerInfo _info;
    private readonly List<Order> _orders;

    public CustomerPrinter(Customer customer)
    {
        _info = customer.Info;
        _orders = customer.Orders;
    }
    public void PrintInfo()
    {
        Console.WriteLine();
        Console.Write("Username: " + _info.GetInfo<string>("Username") + "");
        if (_info.GetInfo<string>("Password") != null)
        {
            Console.Write(", Password: " + _info.GetInfo<string>("Password"));
        }
        if (_info.GetInfo<string>("FirstName") != null)
        {
            Console.Write(", First Name: " + _info.GetInfo<string>("FirstName"));
        }
        if (_info.GetInfo<string>("LastName") != null)
        {
            Console.Write(", Last Name: " + _info.GetInfo<string>("LastName"));
        }
        if (_info.GetInfo<string>("Email") != null)
        {
            Console.Write(", Email: " + _info.GetInfo<string>("Email"));
        }
        if (_info.GetInfo<int>("Age") != -1)
        {
            Console.Write(", Age: " + _info.GetInfo<int>("Age"));
        }
        if (_info.GetInfo<string>("Address") != null)
        {
            Console.Write(", Address: " + _info.GetInfo<string>("Address"));
        }
        if (_info.GetInfo<string>("PhoneNumber") != null)
        {
            Console.Write(", Phone Number: " + _info.GetInfo<string>("PhoneNumber"));
        }
        Console.WriteLine(", Funds: " + _info.GetInfo<int>("Funds"));
        Console.WriteLine();
    }

    public void PrintOrders()
    {
        Console.WriteLine();
        foreach (Order order in _orders)
        {
            order.PrintInfo();
        }
        Console.WriteLine();
    }

    public void PrintOtherStuff()
    {
        throw new NotImplementedException();
    }
}