namespace WebShopCleanCode;

public class CustomerBuilder
{
    private string? _choice;
    private string? _password;
    private string? _username;
    private string? _firstName;
    private string? _lastName;
    private string? _email;
    private int _age;
    private string? _address;
    private string? _phoneNumber;

    public CustomerBuilder()
    {
        _choice = "";
        _age = -1;
    }

    public CustomerBuilder SetUsername(string username)
    {
        _username = username;
        return this;
    }
}