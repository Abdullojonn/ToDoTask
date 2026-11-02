namespace ToDoApp.CommandHandlers;

public class ShowTasksCommandHandler(ITaskStorage storage, UserSession userSession, Logger logger) : ICommandHandler
{
    public string MenuName => "Показать задачи";
    public void HandleCommand()
    {
        if(!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите.");
            return;
        }

        int id = userSession.CurrentUser!.Id;

        List<TodoTask> tasks = storage.LoadTaskByUserId(id);

        if (tasks.Count == 0)
        {
            System.Console.WriteLine("Задач пока нет\n");
            return;
        }

        System.Console.WriteLine("№  | Создана          | Задача                         | Помечена");
        System.Console.WriteLine("----+------------------+---------------------------+---------------------------");

        foreach(var task in tasks)
        {
            System.Console.WriteLine($" {task.Id} | {task.CreatedAt} | {task.Title} | {task.Done}");
        }

        logger.LogInfo($"Пользователь {userSession.CurrentUser.Name} просмотрел список задач");
    }
}