namespace WebShopCleanCode.Interfaces;

public interface IOptionsManager
{
    void DisplayOptions();
    void ClearAllOptions();
    void PrintOptions();
    void SetOptions(List<string> newOptions);
}