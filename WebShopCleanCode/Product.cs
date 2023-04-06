namespace WebShopCleanCode
{
    public class Product
    {
        public string ProductName { get; }
        public int ProductPrice { get; }
        public int NrInStock { get; set; }
        public Product(string productName, int productPrice, int nrInStock)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            NrInStock = nrInStock;
        }
        public bool InStock()
        {
            return NrInStock > 0;
        }
        public void PrintInfo()
        {
            Console.WriteLine(ProductName + ": " + ProductPrice + "kr, " + NrInStock + " in stock.");
        }
    }
}
