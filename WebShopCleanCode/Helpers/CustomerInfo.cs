using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Helpers;

public class CustomerInfo : ICustomerInfo
{
    private readonly Dictionary<string, object> _info;
    
    public CustomerInfo(string username, string password, string firstName, string lastName, string email, int age, string address, string phoneNumber)
    {
        _info = new Dictionary<string, object>
        {
            { "Username", username },
            { "Password", password },
            { "FirstName", firstName },
            { "LastName", lastName },
            { "Email", email },
            { "Age", age },
            { "Address", address },
            { "PhoneNumber", phoneNumber },
            { "Funds", 0 },
        };
    }
    public T GetInfo<T>(string key)
    {
        ValidateKey(key);
        return GetValueOfType<T>(key);
    }
    
    public void SetInfo<T>(string key, T newValue)
    {
        ValidateKey(key);
        SetValueOfType(key, newValue);
    }

    private void SetValueOfType<T>(string key, T newValue)
    {
        if (_info[key] is T)
        {
            _info[key] = newValue;
        }
        else
        {
            throw new ArgumentException($"The value for key {key} is not of the expected type {typeof(T)}.");
        }
    }

    private T GetValueOfType<T>(string key)
    {
        if (_info[key] is T || _info[key] == null)
        {
            return (T)_info[key];
        }

        throw new ArgumentException($"The value for key {key} is not of the expected type {typeof(T)}.");
    }

    private void ValidateKey(string key)
    {
        if (!_info.ContainsKey(key)) throw new ArgumentException("Invalid key: " + key);
    }
}