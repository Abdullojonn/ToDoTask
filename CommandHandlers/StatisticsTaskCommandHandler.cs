using ToDoApp.CommandHandlers;

namespace ToDoApp;

public class StatisticsTaskCommandHandler(ITaskStorage storage, Logger logger, UserSession userSession) : ICommandHandler
{
    public string MenuName => "Статистика задач";

    public void HandleCommand()
    {
        if (!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите.");
            return;
        }

        int totalTasks = 0; 
        int completedTasks = 0;

        int id = userSession.CurrentUser!.Id;

        List<TodoTask> tasks = storage.LoadTaskByUserId(id);

        if (tasks.Count == 0)
        {
            System.Console.WriteLine("Задач пока нет\n");
            return;
        }

        System.Console.WriteLine("Пользователь |  Всего  | Выполнено ");

        foreach(var task in tasks)
        {
            totalTasks++;
            if (task.Done)
            {
                completedTasks++;
            }
        }
        
        System.Console.WriteLine($" {userSession.CurrentUser.Name}      | {totalTasks} | {completedTasks}");
        logger.LogInfo($"Пользователь {userSession.CurrentUser.Name} просмотрел статистику задач");
    }
}