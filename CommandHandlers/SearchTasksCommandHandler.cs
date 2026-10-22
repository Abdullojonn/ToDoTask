namespace ToDoApp.CommandHandlers;

public class SearchTasksCommandHandler(IStorage storage, UserSession userSession, Logger logger) : ICommandHandler
{
    public string MenuName => "Поиск задач";

    public void HandleCommand()
    {
        if(!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите");
            return;
        }

        List<TodoTask> task = storage.LoadTask();

        System.Console.Write("Введите слово для поиска: ");
        string? word = Console.ReadLine();

        if (string.IsNullOrEmpty(word))
        {
            System.Console.WriteLine("Вы ничего не ввели.\n");
            return;
        }


        if (task.Count == 0)
        {
            System.Console.WriteLine("Задач пока нет\n");
            return;
        }

        System.Console.WriteLine("№  | Создана          | Задача                         | Помечена");
        System.Console.WriteLine("----+------------------+---------------------------+---------------------------");  

        bool r = false;
        foreach (var i in task)
        {
            if (i.Title != null && userSession.CurrentUser?.Id == i.UserId)
            {
                if (i.Title.ToLower().Contains(word.ToLower()))
                {
                    System.Console.WriteLine($"{i.Id}  | {i.CreatedAt:yyyy-MM-dd HH:mm}  | {i.Title}  | {i.Done}");
                    r = true;
                }
            }

        }
        if (!r)
        {
            System.Console.WriteLine("Ничего не нашлось.\n");
            return;
        }
        
        logger.LogInfo("ввывел свои задачи.");
        System.Console.WriteLine();

    }
}