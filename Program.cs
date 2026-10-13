using ToDoApp;
using ToDoApp.CommandHandlers;

var strorage = new Storage();
var createTaskHandler = new CreateTaskCommandHandler(strorage);
var showTasksHandler = new ShowTasksCommandHandler(strorage);
var searchTasksHandler = new SearchTasksCommandHandler(strorage);

Dictionary<string, ICommandHandler> commandHandlers = new Dictionary<string, ICommandHandler>
{
    { "1", createTaskHandler },
    { "2", showTasksHandler },
    { "3", searchTasksHandler },
    { "4", delete },
    { "5", setCompleted  }
};


while (true)
{
    ShowMenu();
    string? choice = Console.ReadLine();

    // в зависимости от choice вызвать CreateTask(), ShowTasks() или выйти
    
    if(choice == "0")
    {
        break;
    }
    if (string.IsNullOrEmpty(choice))
    {
        System.Console.WriteLine("Пункт меню не выбран. Попробуйте ещё раз.\n");
        break;
    }

    if (commandHandlers.ContainsKey(choice))
    {
        commandHandlers[choice].HandleCommand();
    }
    else
    {
        System.Console.WriteLine("Неизвестный пункт меню. Попробуйте ещё раз.\n");
    }
}

// здесь методы ShowMenu, CreateTask, ShowTasks, LoadTasks, ...
static void ShowMenu()
{
    System.Console.WriteLine("===== Список задач =====");
    System.Console.WriteLine("1. Создать задачу");
    System.Console.WriteLine("2. Показать список задач");
    System.Console.WriteLine("3. Найти задачу");
    System.Console.WriteLine("0. Выход");
    System.Console.Write("Выберите пункт: ");
}