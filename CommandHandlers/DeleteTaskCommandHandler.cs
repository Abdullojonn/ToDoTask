namespace ToDoApp.CommandHandlers;
using ToDoApp;
using ToDoApp.CommandHandlers;
using System.Globalization;

public class DeleteTasksCommandHandler(IStorage storage): ICommandHandler
{
    private const string FilePath = "tasks.csv";

    public void HandleCommand()
    {
        Console.Write("Введите номер задачи: ");
        string? task_id = Console.ReadLine();

        List<TodoTask> tasks = storage.LoadTask();
        if (tasks.Count <= 0)
        {
            Console.WriteLine("Список задач пуст");
            return;
        }

        bool r = false;
        for(int i = 0; i < tasks.Count; i++)
        {
            if(tasks[i].Id == int.Parse(task_id))
            {
                tasks.RemoveAt(i);
                r = true;
                break;
            }
        }

        if (!r)
        {
            System.Console.WriteLine("Такой Задачи нет");
            return;
        }

        List<string> csvLines = new List<string>();

        foreach (var task in tasks)
        {
            string line = $"{task.Id},{task.Title},{task.CreatedAt:yyyy-MM-dd HH:mm},{task.Done}";

            csvLines.Add(line);
        }

        File.WriteAllLines(FilePath, csvLines);
        System.Console.WriteLine("Задача Успешно Удалено!\n");
    }
}