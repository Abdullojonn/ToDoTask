using System.Globalization;

namespace ToDoApp;

internal class Storage : IStorage
{
    private const string FilePath = "tasks.csv";

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
        if (!File.Exists(FilePath))
        {
            task.Id = 1;
        }
        else
        {
            task.Id = File.ReadAllLines(FilePath).Length + 1;
        }

        if (!File.Exists(FilePath))
        {
            File.Create(FilePath).Close();
        }

        string line = ToCsvLine(task);

        File.AppendAllText(FilePath, line + "\n");
    }

    private static string ToCsvLine(TodoTask task)
    {
        return $"{task.Id},{task.Title},{task.CreatedAt:yyyy-MM-dd HH:mm}";
    }

    private static TodoTask FromCsvLine(string line)
    {
        TodoTask task = new TodoTask();
        string[] parts = line.Split(",");

        task.Id = int.Parse(parts[0]);
        task.Title = parts[1];
        task.CreatedAt = DateTime.ParseExact(parts[2], "yyyy-MM-dd HH:mm", CultureInfo.InstalledUICulture);

        return task;
    }

    public List<TodoTask> Find(string word)
    {
        throw new NotImplementedException();
    }
}
