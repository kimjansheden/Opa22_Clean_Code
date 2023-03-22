namespace WebShopCleanCode;

public class RightCommand : Interfaces.ICommand
{
    private readonly WebShop _webShop;
    public RightCommand(WebShop webShop)
    {
        _webShop = webShop;
    }
    public void Execute()
    {
        if (_webShop.currentChoice < _webShop.amountOfOptions)
        {
            _webShop.currentChoice++;
        }
    }
}