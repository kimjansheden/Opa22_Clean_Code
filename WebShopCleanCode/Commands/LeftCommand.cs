namespace WebShopCleanCode;

public class LeftCommand : Interfaces.ICommand
{
    private readonly WebShop _webShop;
    public LeftCommand(WebShop webShop)
    {
        _webShop = webShop;
    }
    public void Execute()
    {
        if (_webShop.currentChoice > 1)
        {
            _webShop.currentChoice--;
        }
    }
}