using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Enums;

namespace WebShopCleanCode.States.MenuStates;

public class SortMenuState : MenuState
{
    private bool _ascending;
    private bool _descending;
    protected override string DisplayMessage => ((DefaultStrings)Strings).Sort.How;

    public SortMenuState(App app, WebShop webShop) : base(app, webShop)
    {
    }
    protected internal override void Initialize()
    {
        _ascending = true;
        _descending = false;
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
        WebShop.Sort(SortBy.Price, _ascending);
        PrintOkGoBack();
    }

    private void PriceDescending()
    {
        WebShop.Sort(SortBy.Price, _descending);
        PrintOkGoBack();
    }

    private void NameAscending()
    {
        WebShop.Sort(SortBy.Name, _ascending);
        PrintOkGoBack();
    }

    private void NameDescending()
    {
        WebShop.Sort(SortBy.Name, _descending);
        PrintOkGoBack();
    }

    private void PrintOkGoBack()
    {
        PrintMessageWithPadding(((DefaultStrings)Strings).Sort.WaresSorted);
        App.Commands["back"].Execute();
    }
}