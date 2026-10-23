namespace ToDoApp.CommandHandlers;

public interface ICommandHandler
{
    public string MenuName { get; }


    void HandleCommand();
}
