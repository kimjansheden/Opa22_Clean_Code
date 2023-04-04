using WebShopCleanCode.AbstractClasses;

namespace WebShopCleanCode.States.MenuStates;

public class SortMenuState : MenuState
{
    protected override string DisplayMessage => ((DefaultStrings)Strings).Sort.How;

    public SortMenuState(App app, WebShop webShop) : base(app, webShop)
    {
        
    }
    protected internal override void Initialize()
    {
        OptionActions = new Dictionary<int, Action>
        {
            { 1, NameDescending },
            { 2, NameAscending },
            { 3, PriceDescending },
            { 4, PriceAscending }
        };
        Options = new List<string>
        {
            ((DefaultStrings)Strings).Sort.Option1,
            ((DefaultStrings)Strings).Sort.Option2,
            ((DefaultStrings)Strings).Sort.Option3,
            ((DefaultStrings)Strings).Sort.Option4
        };
        CurrentChoice = 1;
    }
    private void PriceAscending()
    {
        WebShop.Sort("price", true);
        PrintOkGoBack();
    }

    private void PriceDescending()
    {
        WebShop.Sort("price", false);
        PrintOkGoBack();
    }

    private void NameAscending()
    {
        WebShop.Sort("name", true);
        PrintOkGoBack();
    }

    private void NameDescending()
    {
        WebShop.Sort("name", false);
        PrintOkGoBack();
    }

    private void PrintOkGoBack()
    {
        PrintMessageWithPadding(((DefaultStrings)Strings).Sort.WaresSorted);
        App.Commands["back"].Execute();
    }
}