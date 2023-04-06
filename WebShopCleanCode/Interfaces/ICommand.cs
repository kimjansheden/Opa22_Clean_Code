namespace WebShopCleanCode.Interfaces;

// In order to have a dynamic flow of actions the customers makes when they interact with the app, I chose to implement the Command Design Pattern. The Command Pattern together with the State Pattern helped me get rid of the switch cases and if statements that the old code used to navigate in the menu and perform actions.
public interface ICommand
{
    void Execute();
    ICommand Initialize(IApp app);
}