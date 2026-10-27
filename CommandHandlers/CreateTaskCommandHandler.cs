using ToDoApp.CommandHandlers;

namespace ToDoApp;

public class CreateTaskCommandHandler(ITaskStorage storage, Logger logger, UserSession userSession) : ICommandHandler
{
    public string MenuName => "Создать задачу";

    public void HandleCommand()
    {
        if (!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите.");
            return;
        }

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
        task.UserId = userSession.CurrentUser!.Id;

        int cnt = 1;
        foreach(var i in storage.LoadTask())
        {
            if(i.UserId == task.UserId)
            {
                cnt ++;
            }
        }
        task.Id = cnt;

        storage.SaveTask(task);
        System.Console.WriteLine($"Задача №{task.Id} создана.\n");


        logger.LogInfo($"Создана задача {task.Id} от пользователья {userSession.CurrentUser.Name}");
    }
}