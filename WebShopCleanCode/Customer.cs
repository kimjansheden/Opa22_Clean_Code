using WebShopCleanCode.Helpers;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode
{
    public class Customer
    {
        private readonly ICustomerInfo _info;
        private readonly ICustomerPrinter _printer;
        private readonly List<Order> _orders;

        public ICustomerInfo Info => _info;

        public List<Order> Orders => _orders;

        public ICustomerPrinter Printer => _printer;

        public Customer(ICustomerInfo info)
        {
            _info = info;
            _printer = new CustomerPrinter(this);
            _orders = new List<Order>();
        }

        public bool CanAfford(int price)
        {
            return _info.GetInfo<int>("Funds") >= price;
        }

        public bool CheckPassword(string password)
        {
            if (password == null)
            {
                return true;
            }
            return password.Equals(_info.GetInfo<string>("Password"));
        }

        public void PrintInfo()
        {
            _printer.PrintInfo();
        }

        public void PrintOrders()
        {
            _printer.PrintOrders(_orders);
        }
    }
}
