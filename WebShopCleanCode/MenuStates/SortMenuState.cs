using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.MenuStates;

public class SortMenuState : MenuState
{
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
        CurrentChoice = 1;
    }
    private void PriceAscending()
    {
        _webShop.Sort("price", true);
        PrintOkGoBack();
    }

    private void PriceDescending()
    {
        _webShop.Sort("price", false);
        PrintOkGoBack();
    }

    private void NameAscending()
    {
        _webShop.Sort("name", true);
        PrintOkGoBack();
    }

    private void NameDescending()
    {
        _webShop.Sort("name", false);
        PrintOkGoBack();
    }

    protected internal override void DisplayOptions()
    {
        _webShopMenu.SetOptions(_options);
        _webShopMenu.AmountOfOptions = 4;
        Console.WriteLine(_strings.Sort.How);
        _webShopMenu.PrintOptions();
    }
    private void PrintOkGoBack()
    {
        PrintMessageWithPadding(_strings.Sort.WaresSorted);
        _webShopMenu.Commands["back"].Execute();
    }
}