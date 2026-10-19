namespace ToDoApp.CommandHandlers;

public class SearchTasksCommandHandler(IStorage storage, UserSession userSession) : ICommandHandler
{
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

        List<TodoTask> task = storage.LoadTask();

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
            if (i.Title != null)
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
        System.Console.WriteLine();
    }
}