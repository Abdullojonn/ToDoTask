using System.Globalization;

const string FilePath = "tasks.csv";

while (true)
{
    ShowMenu();
    string? choice = Console.ReadLine();

    // в зависимости от choice вызвать CreateTask(), ShowTasks() или выйти
    
    if(choice == "0")
    {
        break;
    }

    else if(choice == "1")
    {
        CreateTask();
    }
    
    else if(choice == "2")
    {
        ShowTasks();
    }
    else if(choice == "" || choice == null || choice != "1" || choice != "2")
    {
        System.Console.WriteLine("Неизвестный пункт меню. Попробуйте ещё раз.");
    }
}

// здесь методы ShowMenu, CreateTask, ShowTasks, LoadTasks, ...
static void ShowMenu()
{
    System.Console.WriteLine("===== Список задач =====");
    System.Console.WriteLine("1. Создать задачу");
    System.Console.WriteLine("2. Показать список задач");
    System.Console.WriteLine("0. Выход");
    System.Console.Write("Выберите пункт: ");
}

static List<TodoTask> LoadTask()
{
    List<TodoTask> task = new List<TodoTask>();

    if (!File.Exists(FilePath))
    {
        return task;
    }

    string[] parts = File.ReadAllLines(FilePath);

    foreach(var i in parts)
    {
        task.Add(FromCsvLine(i));
    }
    return task;
}

static void ShowTasks()
{
    List<TodoTask> task = LoadTask();

    if(task.Count == 0)
    {
        System.Console.WriteLine("Задач пока нет");
        return;
    }

    System.Console.WriteLine("№  | Создана          | Задача");
    System.Console.WriteLine("----+------------------+---------------------------");
    
    foreach(var i in task)
    {
        System.Console.WriteLine($"{i.Id}  | {i.CreatedAt:yyyy-MM-dd HH:mm} | {i.Title}");
    }
}
static void CreateTask()
{
    System.Console.Write("Введите название задачи: ");
    
    string? title = Console.ReadLine();

    TodoTask task = new TodoTask();

    task.Title = title;

    task.CreatedAt = DateTime.Now;

    if (!File.Exists(FilePath))
    {
        task.Id = 1;
    }
    else
    {
        task.Id = File.ReadAllLines(FilePath).Length + 1;
    }

    SaveTask(task);
    System.Console.WriteLine($"Задача №{task.Id} создана.");
}

static string ToCsvLine(TodoTask task)
{
    return $"{task.Id},{task.Title},{task.CreatedAt:yyyy-MM-dd HH:mm}";
}

static TodoTask FromCsvLine(string line)
{
    TodoTask task = new TodoTask();
    string[] parts = line.Split(",");
    
    task.Id = int.Parse(parts[0]);
    task.Title = parts[1];
    task.CreatedAt = DateTime.ParseExact(parts[2], "yyyy-MM-dd HH:mm", CultureInfo.InstalledUICulture);

    return task;
//"1,Купить хлеб,2026-10-09 18:30"
}

static void SaveTask(TodoTask task)
{
    if (!File.Exists(FilePath))
    {
        File.Create(FilePath).Close();
    }

    string line = ToCsvLine(task);

    File.AppendAllText(FilePath, line + "\n");

}

public class TodoTask
{
    public int Id;
    public string? Title;
    public DateTime CreatedAt;
}
