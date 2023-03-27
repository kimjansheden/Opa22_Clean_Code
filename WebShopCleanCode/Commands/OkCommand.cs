using WebShopCleanCode.Interfaces;
namespace WebShopCleanCode;

public class OkCommand : ICommand
{
    private readonly WebShop _webShop;
    private readonly WebShopMenu _webShopMenu;
    private Strings _strings;
    private Dictionary<int, IMenu> _menuMap;
    public OkCommand(WebShop webShop, WebShopMenu webShopMenu)
    {
        _webShop = webShop;
        _webShopMenu = webShopMenu;
        _strings = webShopMenu.Strings;

        var choice = 1;
        _menuMap = new Dictionary<int, IMenu>();
        foreach (var menu in _webShopMenu.Menus)
        {
            _menuMap.Add(choice++, menu.Value);
        }
    }

    public void Execute()
    {
        _webShopMenu.CurrentState.ExecuteOption(_webShopMenu.CurrentChoice);
    }
    // public void Execute()
    // {
    //     if (_webShopMenu.CurrentMenu.Equals(_strings.MainMenu))
    //     {
    //         switch (_webShopMenu.CurrentChoice)
    //         {
    //             case 1:
    //                 _menuMap[_webShopMenu.CurrentChoice].Run();
    //                 break;
    //             case 2:
    //                 if (_webShop.currentCustomer != null)
    //                 {
    //                     _webShopMenu.Options[0] = "See your orders";
    //                     _webShopMenu.Options[1] = "Set your info";
    //                     _webShopMenu.Options[2] = "Add funds";
    //                     _webShopMenu.Options[3] = "";
    //                     _webShopMenu.AmountOfOptions = 3;
    //                     _webShopMenu.CurrentChoice = 1;
    //                     _strings.MainMenuWhat = "What would you like to do?";
    //                     _webShopMenu.CurrentMenu = "customer menu";
    //                 }
    //                 else
    //                 {
    //                     Console.WriteLine();
    //                     Console.WriteLine("Nobody is logged in.");
    //                     Console.WriteLine();
    //                 }
    //                 break;
    //             case 3:
    //                 
    //                 break;
    //             default:
    //                 Console.WriteLine();
    //                 Console.WriteLine("Not an option.");
    //                 Console.WriteLine();
    //                 break;
    //         }
    //     }
    //     else if (_webShopMenu.CurrentMenu.Equals("customer menu")) {
    //         switch (_webShopMenu.CurrentChoice)
    //         {
    //             case 1:
    //                 _webShop.currentCustomer.PrintOrders();
    //                 break;
    //             case 2:
    //                 _webShop.currentCustomer.PrintInfo();
    //                 break;
    //             case 3:
    //                 Console.WriteLine("How many funds would you like to add?");
    //                 string amountString = Console.ReadLine();
    //                 try
    //                 {
    //                     int amount = int.Parse(amountString);
    //                     if (amount < 0)
    //                     {
    //                         Console.WriteLine();
    //                         Console.WriteLine("Don't add negative amounts.");
    //                         Console.WriteLine();
    //                     }
    //                     else
    //                     {
    //                         _webShop.currentCustomer.Funds += amount;
    //                         Console.WriteLine();
    //                         Console.WriteLine(amount + " added to your profile.");
    //                         Console.WriteLine();
    //                     }
    //                 }
    //                 catch (FormatException e)
    //                 {
    //                     Console.WriteLine();
    //                     Console.WriteLine("Please write a number next time.");
    //                     Console.WriteLine();
    //                 }
    //                 break;
    //             default:
    //                 Console.WriteLine();
    //                 Console.WriteLine("Not an option.");
    //                 Console.WriteLine();
    //                 break;
    //         }
    //     }
    //     else if (_webShopMenu.CurrentMenu.Equals("sort menu"))
    //     {
    //         bool back = true;
    //         switch (_webShopMenu.CurrentChoice)
    //         {
    //             case 1:
    //                 _webShop.bubbleSort("name", false);
    //                 Console.WriteLine();
    //                 Console.WriteLine("Wares sorted.");
    //                 Console.WriteLine();
    //                 break;
    //             case 2:
    //                 _webShop.bubbleSort("name", true);
    //                 Console.WriteLine();
    //                 Console.WriteLine("Wares sorted.");
    //                 Console.WriteLine();
    //                 break;
    //             case 3:
    //                 _webShop.bubbleSort("price", false);
    //                 Console.WriteLine();
    //                 Console.WriteLine("Wares sorted.");
    //                 Console.WriteLine();
    //                 break;
    //             case 4:
    //                 _webShop.bubbleSort("price", true);
    //                 Console.WriteLine();
    //                 Console.WriteLine("Wares sorted.");
    //                 Console.WriteLine();
    //                 break;
    //             default:
    //                 back = false;
    //                 Console.WriteLine();
    //                 Console.WriteLine("Not an option.");
    //                 Console.WriteLine();
    //                 break;
    //         }
    //         if (back)
    //         {
    //             _webShopMenu.Options[0] = "See all wares";
    //             _webShopMenu.Options[1] = "Purchase a ware";
    //             _webShopMenu.Options[2] = "Sort wares";
    //             if (_webShop.currentCustomer == null)
    //             {
    //                 _webShopMenu.Options[3] = "Login";
    //             }
    //             else
    //             {
    //                 _webShopMenu.Options[3] = "Logout";
    //             }
    //             _webShopMenu.AmountOfOptions = 4;
    //             _webShopMenu.CurrentChoice = 1;
    //             _webShopMenu.CurrentMenu = "wares menu";
    //             _strings.MainMenuWhat = "What would you like to do?";
    //         }
    //     }
    //     else if (_webShopMenu.CurrentMenu.Equals("wares menu"))
    //     {
    //         switch (_webShopMenu.CurrentChoice)
    //         {
    //             case 1:
    //                
    //                 break;
    //             case 2:
    //                 
    //                 break;
    //             case 3:
    //                 
    //                 break;
    //             case 4:
    //                 if (_webShop.currentCustomer == null)
    //                 
    //                 }
    //                 else
    //                 {
    //                     
    //                 }
    //                 break;
    //             case 5:
    //                 break;
    //             default:
    //                 Console.WriteLine();
    //                 Console.WriteLine("Not an option.");
    //                 Console.WriteLine();
    //                 break;
    //         }
    //     }
    //     else if (_webShopMenu.CurrentMenu.Equals("login menu"))
    //     {
    //         switch (_webShopMenu.CurrentChoice)
    //         {
    //             case 1:
    //                 Console.WriteLine("A keyboard appears.");
    //                 Console.WriteLine("Please input your username.");
    //                 _webShopMenu.Username = Console.ReadLine();
    //                 Console.WriteLine();
    //                 break;
    //             case 2:
    //                 Console.WriteLine("A keyboard appears.");
    //                 Console.WriteLine("Please input your password.");
    //                 _webShopMenu.Password = Console.ReadLine();
    //                 Console.WriteLine();
    //                 break;
    //             case 3:
    //                 if (_webShopMenu.Username == null || _webShopMenu.Password == null)
    //                 {
    //                     Console.WriteLine();
    //                     Console.WriteLine("Incomplete data.");
    //                     Console.WriteLine();
    //                 }
    //                 else
    //                 {
    //                     bool found = false;
    //                     foreach (Customer customer in _webShop.customers)
    //                     {
    //                         if (_webShopMenu.Username.Equals(customer.Username) && customer.CheckPassword(_webShopMenu.Password))
    //                         {
    //                             Console.WriteLine();
    //                             Console.WriteLine(customer.Username + " logged in.");
    //                             Console.WriteLine();
    //                             _webShop.currentCustomer = customer;
    //                             found = true;
    //                             _webShopMenu.Options[0] = "See Wares";
    //                             _webShopMenu.Options[1] = "Customer Info";
    //                             if (_webShop.currentCustomer == null)
    //                             {
    //                                 _webShopMenu.Options[2] = "Login";
    //                             }
    //                             else
    //                             {
    //                                 _webShopMenu.Options[2] = "Logout";
    //                             }
    //                             _strings.MainMenuWhat = "What would you like to do?";
    //                             _webShopMenu.CurrentMenu = _strings.MainMenu;
    //                             _webShopMenu.CurrentChoice = 1;
    //                             _webShopMenu.AmountOfOptions = 3;
    //                             break;
    //                         }
    //                     }
    //                     if (found == false)
    //                     {
    //                         Console.WriteLine();
    //                         Console.WriteLine("Invalid credentials.");
    //                         Console.WriteLine();
    //                     }
    //                 }
    //                 break;
    //             case 4:
    //                 Console.WriteLine("Please write your username.");
    //                 string newUsername = Console.ReadLine();
    //                 foreach (Customer customer in _webShop.customers)
    //                 {
    //                     if (customer.Username.Equals(_webShopMenu.Username))
    //                     {
    //                         Console.WriteLine();
    //                         Console.WriteLine("Username already exists.");
    //                         Console.WriteLine();
    //                         break;
    //                     }
    //                 }
    //                 // Would have liked to be able to quit at any time in here.
    //                 string choice = "";
    //                 bool next = false;
    //                 string newPassword = null;
    //                 string firstName = null;
    //                 string lastName = null;
    //                 string email = null;
    //                 int age = -1;
    //                 string address = null;
    //                 string phoneNumber = null;
    //                 while (true)
    //                 {
    //                     Console.WriteLine("Do you want a password? y/n");
    //                     choice = Console.ReadLine();
    //                     if (choice.Equals("y"))
    //                     {
    //                         while (true)
    //                         {
    //                             Console.WriteLine("Please write your password.");
    //                             newPassword = Console.ReadLine();
    //                             if (newPassword.Equals(""))
    //                             {
    //                                 Console.WriteLine();
    //                                 Console.WriteLine("Please actually write something.");
    //                                 Console.WriteLine();
    //                                 continue;
    //                             }
    //                             else
    //                             {
    //                                 next = true;
    //                                 break;
    //                             }
    //                         }
    //                     }
    //                     if (choice.Equals("n") || next)
    //                     {
    //                         next = false;
    //                         break;
    //                     }
    //                     Console.WriteLine();
    //                     Console.WriteLine("y or n, please.");
    //                     Console.WriteLine();
    //                 }
    //                 while (true)
    //                 {
    //                     Console.WriteLine("Do you want a first name? y/n");
    //                     choice = Console.ReadLine();
    //                     if (choice.Equals("y"))
    //                     {
    //                         while (true)
    //                         {
    //                             Console.WriteLine("Please write your first name.");
    //                             firstName = Console.ReadLine();
    //                             if (firstName.Equals(""))
    //                             {
    //                                 Console.WriteLine();
    //                                 Console.WriteLine("Please actually write something.");
    //                                 Console.WriteLine();
    //                                 continue;
    //                             }
    //                             else
    //                             {
    //                                 next = true;
    //                                 break;
    //                             }
    //                         }
    //                     }
    //                     if (choice.Equals("n") || next)
    //                     {
    //                         next = false;
    //                         break;
    //                     }
    //                     Console.WriteLine();
    //                     Console.WriteLine("y or n, please.");
    //                     Console.WriteLine();
    //                 }
    //                 while (true)
    //                 {
    //                     Console.WriteLine("Do you want a last name? y/n");
    //                     choice = Console.ReadLine();
    //                     if (choice.Equals("y"))
    //                     {
    //                         while (true)
    //                         {
    //                             Console.WriteLine("Please write your last name.");
    //                             lastName = Console.ReadLine();
    //                             if (lastName.Equals(""))
    //                             {
    //                                 Console.WriteLine();
    //                                 Console.WriteLine("Please actually write something.");
    //                                 Console.WriteLine();
    //                                 continue;
    //                             }
    //                             else
    //                             {
    //                                 next = true;
    //                                 break;
    //                             }
    //                         }
    //                     }
    //                     if (choice.Equals("n") || next)
    //                     {
    //                         next = false;
    //                         break;
    //                     }
    //                     Console.WriteLine();
    //                     Console.WriteLine("y or n, please.");
    //                     Console.WriteLine();
    //                 }
    //                 while (true)
    //                 {
    //                     Console.WriteLine("Do you want an email? y/n");
    //                     choice = Console.ReadLine();
    //                     if (choice.Equals("y"))
    //                     {
    //                         while (true)
    //                         {
    //                             Console.WriteLine("Please write your email.");
    //                             email = Console.ReadLine();
    //                             if (email.Equals(""))
    //                             {
    //                                 Console.WriteLine();
    //                                 Console.WriteLine("Please actually write something.");
    //                                 Console.WriteLine();
    //                                 continue;
    //                             }
    //                             else
    //                             {
    //                                 next = true;
    //                                 break;
    //                             }
    //                         }
    //                     }
    //                     if (choice.Equals("n") || next)
    //                     {
    //                         next = false;
    //                         break;
    //                     }
    //                     Console.WriteLine();
    //                     Console.WriteLine("y or n, please.");
    //                     Console.WriteLine();
    //                 }
    //                 while (true)
    //                 {
    //                     Console.WriteLine("Do you want an age? y/n");
    //                     choice = Console.ReadLine();
    //                     if (choice.Equals("y"))
    //                     {
    //                         while (true)
    //                         {
    //                             Console.WriteLine("Please write your age.");
    //                             string ageString = Console.ReadLine();
    //                             try
    //                             {
    //                                 age = int.Parse(ageString);
    //                             }
    //                             catch (FormatException e)
    //                             {
    //                                 Console.WriteLine();
    //                                 Console.WriteLine("Please write a number.");
    //                                 Console.WriteLine();
    //                                 continue;
    //                             }
    //                             next = true;
    //                             break;
    //                         }
    //                     }
    //                     if (choice.Equals("n") || next)
    //                     {
    //                         next = false;
    //                         break;
    //                     }
    //                     Console.WriteLine();
    //                     Console.WriteLine("y or n, please.");
    //                     Console.WriteLine();
    //                 }
    //                 while (true)
    //                 {
    //                     Console.WriteLine("Do you want an address? y/n");
    //                     choice = Console.ReadLine();
    //                     if (choice.Equals("y"))
    //                     {
    //                         while (true)
    //                         {
    //                             Console.WriteLine("Please write your address.");
    //                             address = Console.ReadLine();
    //                             if (address.Equals(""))
    //                             {
    //                                 Console.WriteLine();
    //                                 Console.WriteLine("Please actually write something.");
    //                                 Console.WriteLine();
    //                                 continue;
    //                             }
    //                             else
    //                             {
    //                                 next = true;
    //                                 break;
    //                             }
    //                         }
    //                     }
    //                     if (choice.Equals("n") || next)
    //                     {
    //                         next = false;
    //                         break;
    //                     }
    //                     Console.WriteLine();
    //                     Console.WriteLine("y or n, please.");
    //                     Console.WriteLine();
    //                 }
    //                 while (true)
    //                 {
    //                     Console.WriteLine("Do you want a phone number? y/n");
    //                     choice = Console.ReadLine();
    //                     if (choice.Equals("y"))
    //                     {
    //                         while (true)
    //                         {
    //                             Console.WriteLine("Please write your phone number.");
    //                             phoneNumber = Console.ReadLine();
    //                             if (phoneNumber.Equals(""))
    //                             {
    //                                 Console.WriteLine();
    //                                 Console.WriteLine("Please actually write something.");
    //                                 Console.WriteLine();
    //                                 continue;
    //                             }
    //                             else
    //                             {
    //                                 next = true;
    //                                 break;
    //                             }
    //                         }
    //                     }
    //                     if (choice.Equals("n") || next)
    //                     {
    //                         break;
    //                     }
    //                     Console.WriteLine();
    //                     Console.WriteLine("y or n, please.");
    //                     Console.WriteLine();
    //                 }
    //
    //                 Customer newCustomer = new Customer(newUsername, newPassword, firstName, lastName, email, age, address, phoneNumber);
    //                 _webShop.customers.Add(newCustomer);
    //                 _webShop.currentCustomer = newCustomer;
    //                 Console.WriteLine();
    //                 Console.WriteLine(newCustomer.Username + " successfully added and is now logged in.");
    //                 Console.WriteLine();
    //                 _webShopMenu.Options[0] = "See Wares";
    //                 _webShopMenu.Options[1] = "Customer Info";
    //                 if (_webShop.currentCustomer == null)
    //                 {
    //                     _webShopMenu.Options[2] = "Login";
    //                 }
    //                 else
    //                 {
    //                     _webShopMenu.Options[2] = "Logout";
    //                 }
    //                 _strings.MainMenuWhat = "What would you like to do?";
    //                 _webShopMenu.CurrentMenu = _strings.MainMenu;
    //                 _webShopMenu.CurrentChoice = 1;
    //                 _webShopMenu.AmountOfOptions = 3;
    //                 break;
    //             default:
    //                 Console.WriteLine();
    //                 Console.WriteLine("Not an option.");
    //                 Console.WriteLine();
    //                 break;
    //         }
    //     }
    //     else if (_webShopMenu.CurrentMenu.Equals("purchase menu"))
    //     {
    //         
    //     }
    // }
}