using WebShopCleanCode.AbstractClasses;
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
        CurrentChoice = 1;
    }

    private IState CurrentState
    {
        get => _webShopMenu.CurrentState;
        set => _webShopMenu.CurrentState = value;
    }

    private IState PreviousState
    {
        get => _webShopMenu.PreviousState;
        set => _webShopMenu.PreviousState = value;
    }

    private Dictionary<StatesEnum, IMenuState> States
    {
        get => _webShopMenu.States;
        set => _webShopMenu.States = value;
    }

    private int CurrentChoice
    {
        get => _webShopMenu.CurrentChoice;
        set => _webShopMenu.CurrentChoice = value;
    }
    private List<IState> StateHistory
    {
        get => _webShopMenu.StateHistory;
        set => _webShopMenu.StateHistory = value;
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

    public void ChangeState(StatesEnum stateEnum)
    {
        PreviousState = this;
        CurrentState = States[stateEnum];
        CurrentChoice = 1;
        StateHistory.Add(this);
    }
    private void PrintOkGoBack()
    {
        Console.WriteLine();
        Console.WriteLine("Wares sorted.");
        Console.WriteLine();
        _webShopMenu.Commands["back"].Execute();
    }
}