namespace ToDoApp.CommandHandlers;

public class ShowTasksCommandHandler(IStorage storage, UserSession userSession) : ICommandHandler
{
    public void HandleCommand()
    {
        if(!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите.");
            return;
        }

        int id = userSession.CurrentUser!.Id;

        List<TodoTask> task = storage.LoadTask();

        bool r = false;
        foreach (var i in task)
        {
            if(i.UserId == id)
            {
                r = true;
                break;
            }
        }

        if (!r)
        {
            System.Console.WriteLine("Задач пока нет\n");
            return;
        }

        System.Console.WriteLine("№  | Создана          | Задача                         | Помечена");
        System.Console.WriteLine("----+------------------+---------------------------+---------------------------");    

        foreach (var i in task)
        {
            if(i.UserId != id)
            {
                continue;
            }
            System.Console.WriteLine($"{i.Id}  | {i.CreatedAt:yyyy-MM-dd HH:mm}  | {i.Title}  | {i.Done}");
        }
        System.Console.WriteLine();
    }
}