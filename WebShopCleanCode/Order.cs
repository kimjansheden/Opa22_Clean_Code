namespace WebShopCleanCode
{
    public class Order
    {
        private string ProductName { get; set; }
        private int BoughtFor { get; set; }
        private DateTime PurchaseTime { get; set; }
        public Order(string productName, int boughtFor, DateTime purchaseTime)
        {
            ProductName = productName;
            BoughtFor = boughtFor;
            PurchaseTime = purchaseTime;
        }
        public void PrintInfo()
        {
            Console.WriteLine(ProductName + ", bought for " + BoughtFor + "kr, time: " + PurchaseTime + ".");
        }
    }
}
