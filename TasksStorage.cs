using System.Globalization;

namespace ToDoApp;

internal class TasksStorage : ITaskStorage
{
    public const string FilePath = "tasks.csv";

public List<TodoTask> LoadTask()
{
    List<TodoTask> tasks = new List<TodoTask>();

    if (!File.Exists(FilePath))
    {
        return tasks;
    }

    string[] parts = File.ReadAllLines(FilePath);

    foreach (var line in parts)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            continue;
        }

        var task = FromCsvLine(line);
        if (task != null)
        {
            tasks.Add(task);
        }
    }

    return tasks;
}

    public void SaveTask(TodoTask task)
    {
        List<TodoTask> tasks = LoadTask();

        int mx = 0;
        
        foreach (var t in tasks)
        {
            if(t.UserId == task.UserId && t.Id > mx)
            {
                mx = t.Id;
            }
        }

        task.Id = mx + 1;

        string line = ToCsvLine(task);
        File.AppendAllText(FilePath, line + "\n");
    }

    private static string ToCsvLine(TodoTask task)
    {
        return $"{task.Id},{task.Title},{task.CreatedAt:yyyy-MM-dd HH:mm},{task.Done},{task.UserId}";
    }
private static TodoTask? FromCsvLine(string line)
{
    string[] parts = line.Split(",");

    if (parts.Length < 5)
    {
        Console.WriteLine($"Пропущена некорректная строка: {line}");
        return null;
    }

    TodoTask task = new TodoTask();
    task.Id = int.Parse(parts[0]);
    task.Title = parts[1];
    task.CreatedAt = DateTime.ParseExact(parts[2], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
    task.Done = bool.Parse(parts[3]);
    task.UserId = int.Parse(parts[4]);

    return task;
}

    public bool RemoveTask(int id, int userId)
    {
        List<TodoTask> tasks = LoadTask();

        if (tasks.Count <= 0)
        {
            return true;
        }

        bool r = false;
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].Id == id && tasks[i].UserId == userId)
            {
                tasks.RemoveAt(i);
                r = true;
                break;
            }
        }

        if (!r)
        {
            System.Console.WriteLine("Такой Задачи нет");
            return false;
        }

        List<string> csvLines = new List<string>();

        foreach (var taskk in tasks)
        {
            csvLines.Add(ToCsvLine(taskk));
        }

        File.WriteAllLines(FilePath, csvLines);
        return true;

    }
}
