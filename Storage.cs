using System.Globalization;

namespace ToDoApp;

internal class Storage : IStorage
{
    public const string FilePath = "tasks.csv";

    public List<TodoTask> LoadTask()
    {
        List<TodoTask> task = new List<TodoTask>();

        if (!File.Exists(FilePath))
        {
            return task;
        }

        string[] parts = File.ReadAllLines(FilePath);

        foreach (var i in parts)
        {
            task.Add(FromCsvLine(i));
        }
        return task;
    }

    public void SaveTask(TodoTask task)
    {
        List<TodoTask> tasks = LoadTask();

        int mx = 0;
        
        foreach (var t in tasks)
        {
            if(t.Id > mx)
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

    private static TodoTask FromCsvLine(string line)
    {
        TodoTask task = new TodoTask();
        string[] parts = line.Split(",");

        task.Id = int.Parse(parts[0]);
        task.Title = parts[1];
        task.CreatedAt = DateTime.ParseExact(parts[2], "yyyy-MM-dd HH:mm", CultureInfo.InstalledUICulture);
        task.Done = bool.Parse(parts[3]);
        task.UserId = int.Parse(parts[4]);

        return task;
    }

    public bool RemoveTask(int id)
    {
        List<TodoTask> tasks = LoadTask();
        if (tasks.Count <= 0)
        {
            return true;
        }

        bool r = false;
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].Id == id)
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

        foreach (var task in tasks)
        {
            csvLines.Add(ToCsvLine(task));
        }

        File.WriteAllLines(FilePath, csvLines);
        return true;
    }
}
