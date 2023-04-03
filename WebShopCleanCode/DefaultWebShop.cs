using WebShopCleanCode.AbstractClasses;
namespace WebShopCleanCode
{
    public class DefaultWebShop : WebShop
    {
        private readonly Database _database = new Database();

        /// <summary>
        /// Bubble Sort.
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="ascending"></param>
        public override void Sort(string variable, bool ascending)
        {
            if (variable.Equals("name")) {
                int length = Products.Count;
                for(int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (Products[j].Name.CompareTo(Products[j + 1].Name) < 0)
                            {
                                Product temp = Products[j];
                                Products[j] = Products[j + 1];
                                Products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (Products[j].Name.CompareTo(Products[j + 1].Name) > 0)
                            {
                                Product temp = Products[j];
                                Products[j] = Products[j + 1];
                                Products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
            else if (variable.Equals("price"))
            {
                int length = Products.Count;
                for (int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (Products[j].Price > Products[j + 1].Price)
                            {
                                Product temp = Products[j];
                                Products[j] = Products[j + 1];
                                Products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (Products[j].Price < Products[j + 1].Price)
                            {
                                Product temp = Products[j];
                                Products[j] = Products[j + 1];
                                Products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
        }

        protected internal override void Initialize()
        {
            Products = _database.GetProducts();
            Customers = _database.GetCustomers();
        }
    }
}
