namespace ToDoApp.CommandHandlers;
using ToDoApp;
using ToDoApp.CommandHandlers;
using System.Globalization;
using System.Runtime.CompilerServices;

public class DoneTasksCommandHandler(ITaskStorage storage, UserSession userSession) : ICommandHandler
{
    //private const string FilePath = "tasks.csv";
    public void HandleCommand()
    {
        if(!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите");
            return;
        }

        System.Console.Write("Введите номер задачи: ");
        string? input = Console.ReadLine();

        List<TodoTask> tasks = storage.LoadTask();

        if(tasks.Count <= 0){
            System.Console.WriteLine("Список задач пуст");
            return;
        }

        var result = int.TryParse(input, out int taskId);
        if (!result)
        {
            System.Console.WriteLine("Введите число");
            return;
        }

        foreach (var task in tasks)
        {
            if (task.Id == taskId && task.UserId == userSession.CurrentUser?.Id)
            {
                task.Done = true;
            }
        }

        List<string> csv = new List<string>();

        foreach(var i in tasks)
        {
            string? line = $"{i.Id},{i.Title},{i.CreatedAt:yyyy-MM-dd HH:mm},{i.Done},{i.UserId}";
            csv.Add(line);
        }

        File.WriteAllLines(TasksStorage.FilePath, csv);
        System.Console.WriteLine("Задача Успешно Помечена.\n");
    }
}