using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Enums;

namespace WebShopCleanCode
{
    public class DefaultWebShop : WebShop
    {
        private readonly Database _database = new Database();

        /// <summary>
        /// Bubble Sort.
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        public override void Sort(Enum sortBy, bool ascending)
        {
            if (sortBy.Equals(SortBy.Name)) {
                int length = Products.Count;
                for(int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (String.Compare(Products[j].Name, Products[j + 1].Name, StringComparison.Ordinal) > 0)
                            {
                                Product temp = Products[j];
                                Products[j] = Products[j + 1];
                                Products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (String.Compare(Products[j].Name, Products[j + 1].Name, StringComparison.Ordinal) < 0)
                            {
                                Product temp = Products[j];
                                Products[j] = Products[j + 1];
                                Products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted)
                    {
                        break;
                    }
                }
            }
            else if (sortBy.Equals(SortBy.Price))
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
                    if (sorted)
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
