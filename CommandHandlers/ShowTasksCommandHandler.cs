namespace ToDoApp.CommandHandlers;

public class ShowTasksCommandHandler(IStorage storage) : ICommandHandler
{
    public void HandleCommand()
    {
        List<TodoTask> task = storage.LoadTask();

        if (task.Count == 0)
        {
            System.Console.WriteLine("Задач пока нет\n");
            return;
        }

        System.Console.WriteLine("№  | Создана          | Задача                         | Помечена");
        System.Console.WriteLine("----+------------------+---------------------------+---------------------------");    

        foreach (var i in task)
        {
            System.Console.WriteLine($"{i.Id}  | {i.CreatedAt:yyyy-MM-dd HH:mm}  | {i.Title}  | {i.Done}");
        }
        System.Console.WriteLine();
    }
}