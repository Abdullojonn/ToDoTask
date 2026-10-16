using ToDoApp.CommandHandlers;

namespace ToDoApp;

public class CreateTaskCommandHandler(IStorage storage, Logger logger) : ICommandHandler
{
    public void HandleCommand()
    {
        System.Console.Write("Введите название задачи: ");

        string? title = Console.ReadLine();
        if (string.IsNullOrEmpty(title))
        {
            logger.LogError("Название задачи не введено");
            return;
        }


        TodoTask task = new TodoTask();
        task.Title = title;
        task.CreatedAt = DateTime.Now;

        storage.SaveTask(task);
        System.Console.WriteLine($"Задача №{task.Id} создана.\n");


        logger.LogInfo("Создана задача");
    }
}