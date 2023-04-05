namespace WebShopCleanCode
{
    public class Database
    {
        // We just pretend this accesses a real database.
        private List<Product> productsInDatabase;
        private List<Customer> customersInDatabase;
        private CustomerBuilder _customerBuilder;
        public Database()
        {
            _customerBuilder = new CustomerBuilder();
            productsInDatabase = new List<Product>();
            productsInDatabase.Add(new Product("Mirror", 300, 2));
            productsInDatabase.Add(new Product("Car", 2000000, 2));
            productsInDatabase.Add(new Product("Candle", 50, 2));
            productsInDatabase.Add(new Product("Computer", 100000, 2));
            productsInDatabase.Add(new Product("Game", 599, 2));
            productsInDatabase.Add(new Product("Painting", 399, 2));
            productsInDatabase.Add(new Product("Chair", 500, 2));
            productsInDatabase.Add(new Product("Table", 1000, 2));
            productsInDatabase.Add(new Product("Bed", 20000, 2));

            customersInDatabase = new List<Customer>();
            customersInDatabase.Add(_customerBuilder.SetUsername("jimmy").SetPassword("jimisthebest").SetFirstName("Jimmy").SetLastName("Jamesson").SetEmail("jj@mail.com").SetAge(22).SetAddress("Big Street 5").SetPhoneNumber("123456789").Build());
            customersInDatabase.Add(_customerBuilder.SetUsername("jake").SetPassword("jake123").SetFirstName("Jake").Build());
        }

        public List<Product> GetProducts()
        {
            return productsInDatabase;
        }

        public List<Customer> GetCustomers()
        {
            return customersInDatabase;
        }
    }
}
