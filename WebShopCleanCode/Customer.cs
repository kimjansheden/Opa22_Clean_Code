namespace WebShopCleanCode
{
    public class Customer
    {
        public string Username { get; }
        private string Password { get; }
        private string FirstName { get; }
        private string LastName { get; }
        private string Email { get; }
        private int Age { get; }
        private string Address { get; }
        private string PhoneNumber { get; }
        public int Funds { get; set; }
        public List<Order> Orders { get; }
        public Customer(string username, string password, string firstName, string lastName, string email, int age, string address, string phoneNumber)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Address = address;
            PhoneNumber = phoneNumber;
            Orders = new List<Order>();
            Funds = 0;
        }

        public bool CanAfford(int price)
        {
            return Funds >= price;
        }

        public bool CheckPassword(string password)
        {
            if (password == null)
            {
                return true;
            }
            return password.Equals(Password);
        }

        public void PrintInfo()
        {
            Console.WriteLine();
            Console.Write("Username: " + Username + "");
            if (Password != null)
            {
                Console.Write(", Password: " + Password);
            }
            if (FirstName != null)
            {
                Console.Write(", First Name: " + FirstName);
            }
            if (LastName != null)
            {
                Console.Write(", Last Name: " + LastName);
            }
            if (Email != null)
            {
                Console.Write(", Email: " + Email);
            }
            if (Age != -1)
            {
                Console.Write(", Age: " + Age);
            }
            if (Address != null)
            {
                Console.Write(", Address: " + Address);
            }
            if (PhoneNumber != null)
            {
                Console.Write(", Phone Number: " + PhoneNumber);
            }
            Console.WriteLine(", Funds: " + Funds);
            Console.WriteLine();
        }

        public void PrintOrders()
        {
            Console.WriteLine();
            foreach (Order order in Orders)
            {
                order.PrintInfo();
            }
            Console.WriteLine();
        }
    }
}
