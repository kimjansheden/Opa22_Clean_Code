namespace WebShopCleanCode.Interfaces;

public interface ICommandExecutor
{
    void ExecuteCommandIfExists(string input);
}