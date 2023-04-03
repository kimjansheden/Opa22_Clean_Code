namespace WebShopCleanCode;

public class CustomerBuilder
{
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
        _age = -1;
    }

    public CustomerBuilder SetUsername(string username)
    {
        _username = username;
        return this;
    }

    public CustomerBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public CustomerBuilder SetLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public CustomerBuilder SetPassword(string password)
    {
        _password = password;
        return this;
    }

    public CustomerBuilder SetEmail(string email)
    {
        _email = email;
        return this;
    }

    public CustomerBuilder SetAge(int age)
    {
        _age = age;
        return this;
    }

    public CustomerBuilder SetAddress(string address)
    {
        _address = address;
        return this;
    }

    public CustomerBuilder SetPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public Customer Build()
    {
        return new Customer(_username, _password, _firstName, _lastName, _email, _age, _address, _phoneNumber);
    }
}