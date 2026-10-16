using ToDoApp;
using ToDoApp.CommandHandlers;

// Dependency Injection


// handler -> storage -> F

var storage = new Storage();
var createTaskHandler = new CreateTaskCommandHandler(storage);
var showTasksHandler = new ShowTasksCommandHandler(storage);
var searchTasksHandler = new SearchTasksCommandHandler(storage);
var deleteTaskHandler = new DeleteTasksCommandHandler(storage);
var doneTasksHandler = new DoneTasksCommandHandler(storage);

Dictionary<string, ICommandHandler> commandHandlers = new Dictionary<string, ICommandHandler>
{
    { "1", createTaskHandler },
    { "2", showTasksHandler },
    { "3", searchTasksHandler },
    { "4", deleteTaskHandler },
    {"5", doneTasksHandler}
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
    System.Console.WriteLine("4. Удалить задачу");
    System.Console.WriteLine("5. Помечать задачу");
    System.Console.WriteLine("0. Выход");
    System.Console.Write("Выберите пункт: ");
}
