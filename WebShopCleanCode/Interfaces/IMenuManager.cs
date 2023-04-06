namespace WebShopCleanCode.Interfaces;

public interface IMenuManager
{
    void PrintNavigation();
    IMenuManager Initialize(IApp? app);
}