using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Enums;

namespace WebShopCleanCode
{
    public class DefaultWebShop : WebShop
    {
        private readonly Database _database = new Database();
        public override void Initialize()
        {
            Products = _database.GetProducts();
            Customers = _database.GetCustomers();
        }

        /// <summary>
        /// Merge Sort.
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="ascending"></param>
        public override void Sort(Enum sortBy, bool ascending)
        {
            if (sortBy.Equals(SortBy.Name))
            {
                Products = MergeSort(Products, ascending, (a, b) => string.Compare(a.ProductName, b.ProductName, StringComparison.Ordinal));
            }
            else if (sortBy.Equals(SortBy.Price))
            {
                Products = MergeSort(Products, ascending, (a, b) => a.ProductPrice.CompareTo(b.ProductPrice));
            }
        }

        private List<Product> MergeSort(List<Product> products, bool ascending, Func<Product, Product, int> compareFunc)
        {
            // If the list has one or no elements, it's already sorted, so we return it as it is.
            if (products.Count <= 1)
            {
                return products;
            }

            int middleIndex = products.Count / 2;
            List<Product> left = products.GetRange(0, middleIndex);
            List<Product> right = products.GetRange(middleIndex, products.Count - middleIndex);

            left = MergeSort(left, ascending, compareFunc);
            right = MergeSort(right, ascending, compareFunc);

            return Merge(left, right, ascending, compareFunc);
        }

        private List<Product> Merge(List<Product> left, List<Product> right, bool ascending, Func<Product, Product, int> compareFunc)
        {
            List<Product> result = new List<Product>();
            int leftIndex = 0, rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                int comparisonResult = compareFunc(left[leftIndex], right[rightIndex]);

                if (ascending)
                {
                    if (comparisonResult <= 0)
                    {
                        result.Add(left[leftIndex]);
                        leftIndex++;
                    }
                    else
                    {
                        result.Add(right[rightIndex]);
                        rightIndex++;
                    }
                }
                else
                {
                    if (comparisonResult >= 0)
                    {
                        result.Add(left[leftIndex]);
                        leftIndex++;
                    }
                    else
                    {
                        result.Add(right[rightIndex]);
                        rightIndex++;
                    }
                }
            }

            while (leftIndex < left.Count)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Count)
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }

            return result;
        }
    }
}
