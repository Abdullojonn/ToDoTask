using ToDoApp.CommandHandlers;

namespace ToDoApp;

public class CreateTaskCommandHandler(IStorage storage) : ICommandHandler
{
    public void HandleCommand()
    {
        System.Console.Write("Введите название задачи: ");

        string? title = Console.ReadLine();

        TodoTask task = new TodoTask();
        task.Title = title;
        task.CreatedAt = DateTime.Now;

        storage.SaveTask(task);
        System.Console.WriteLine($"Задача №{task.Id} создана.\n");
    }
}