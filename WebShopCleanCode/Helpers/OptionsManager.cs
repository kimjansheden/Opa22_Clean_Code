using WebShopCleanCode.AbstractClasses;
using WebShopCleanCode.Interfaces;

namespace WebShopCleanCode.Helpers;

public class OptionsManager : IOptionsManager
{
    private readonly IApp _app;
    public OptionsManager(IApp app)
    {
        _app = app;
    }

    public void DisplayOptions()
    {
        if (_app.CurrentState is MenuState menuState) menuState.DisplayOptions();
    }
    
    public void ClearAllOptions()
    {
        for (int i = 0; i < _app.Options.Count; i++)
        {
            _app.Options[i] = "";
        }
    }

    public void PrintOptions()
    {
        var optionNum = 1;
        foreach (string option in _app.Options)
        {
            if(!string.IsNullOrEmpty(option))
                Console.WriteLine(optionNum++ + ": " + option);
            if (_app.AmountOfOptions <= 3 && optionNum == 4)
            {
                return;
            }
        }
    }
    public void SetOptions(List<string> newOptions)
    {
        _app.Options.Clear();
        _app.Options.AddRange(newOptions);
    }
}