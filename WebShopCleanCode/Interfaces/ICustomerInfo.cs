namespace WebShopCleanCode.Interfaces;

public interface ICustomerInfo
{
    T GetInfo<T>(string key);
    void SetInfo<T>(string key, T newValue);
}