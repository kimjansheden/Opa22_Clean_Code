using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.MenuStates;

public class SortMenuState : IMenuState
{
    private readonly WebShopMenu _webShopMenu;
    private readonly WebShop _webShop;
    private Dictionary<int, Action> _optionActions;
    private string _loginState;
    private Strings _strings;
    private List<string> _options;
    public SortMenuState(WebShopMenu webShopMenu, WebShop webShop)
    {
        _webShopMenu = webShopMenu;
        _webShop = webShop;
        _strings = webShopMenu.Strings;
        _optionActions = new Dictionary<int, Action>
        {
            { 1, NameDescending },
            { 2, NameAscending },
            { 3, PriceDescending },
            { 4, PriceAscending }
        };
        _options = new List<string>
        {
            _webShopMenu.Strings.Sort.Option1,
            _webShopMenu.Strings.Sort.Option2,
            _webShopMenu.Strings.Sort.Option3,
            _webShopMenu.Strings.Sort.Option4
        };
        webShopMenu.CurrentChoice = 1;
    }

    private void PriceAscending()
    {
        _webShop.bubbleSort("price", true);
        PrintOkGoBack();
    }

    private void PrintOkGoBack()
    {
        Console.WriteLine();
        Console.WriteLine("Wares sorted.");
        Console.WriteLine();
        _webShopMenu.Commands["back"].Execute();
    }

    private void PriceDescending()
    {
        _webShop.bubbleSort("price", false);
        PrintOkGoBack();
    }

    private void NameAscending()
    {
        _webShop.bubbleSort("name", true);
        PrintOkGoBack();
    }

    private void NameDescending()
    {
        _webShop.bubbleSort("name", false);
        PrintOkGoBack();
    }

    public void DisplayOptions()
    {
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 4;
        Console.WriteLine(_strings.Sort.How);
        _webShopMenu.PrintOptions();
    }

    public void ExecuteOption(int option)
    {
        _optionActions[option]();
    }
}