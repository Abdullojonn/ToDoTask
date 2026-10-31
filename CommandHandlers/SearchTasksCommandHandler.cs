namespace ToDoApp.CommandHandlers;

public class SearchTasksCommandHandler(ITaskStorage storage, UserSession userSession, Logger logger) : ICommandHandler
{
    public string MenuName => "Поиск задач";

    public void HandleCommand()
    {
        if(!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите");
            return;
        }

        System.Console.Write("Введите слово для поиска: ");
        string? word = Console.ReadLine();

        if (string.IsNullOrEmpty(word))
        {
            System.Console.WriteLine("Вы ничего не ввели.\n");
            return;
        }


        bool found = storage.FindTask(word, userSession.CurrentUser!.Id);

        if (!found)
        {
            System.Console.WriteLine($"Задача не найдена.\n");
            logger.LogInfo($"не нашел задачу {word}.");
            return;
        }

        else
        {

        System.Console.WriteLine("№  | Создана          | Задача                         | Помечена");
        System.Console.WriteLine("----+------------------+---------------------------+---------------------------");    

            List<TodoTask> tasks = storage.LoadTaskByUserId(userSession.CurrentUser!.Id);
            foreach(var task in tasks)
            {
                System.Console.WriteLine($" {task.Id} | {task.CreatedAt} | {task.Title} | {task.Done}");
            }
            logger.LogInfo($"нашел задачу {word}.");
        }
    }
}