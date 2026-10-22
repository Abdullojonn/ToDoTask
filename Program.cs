using Microsoft.Extensions.DependencyInjection;
using ToDoApp;
using ToDoApp.CommandHandlers;

var logger = new Logger();
var storage = new Storage();
var userStorage = new UserStorage();
var session = new UserSession();

var createTaskHandler = new CreateTaskCommandHandler(storage, logger, session);
var showTasksHandler = new ShowTasksCommandHandler(storage, session);
var searchTasksHandler = new SearchTasksCommandHandler(storage, session, logger);
var deleteTaskHandler = new DeleteTasksCommandHandler(storage, session, logger);
var doneTasksHandler = new DoneTasksCommandHandler(storage, session);

var registerHandler = new RegisterUserCommandHandler(userStorage, logger);
var loginHandler = new LoginCommandHandler(userStorage, session, logger);
var logoutHandler = new LogoutCommandHandler(session, logger);


Dictionary<string, ICommandHandler> commandHandlers = new Dictionary<string, ICommandHandler>
{
    commandHandlers.Add((i + 1).ToString(), handlers.ElementAt(i));
}


while (true)
{

    ShowMenu(session, commandHandlers);
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
        Console.Clear();

        commandHandlers[choice].HandleCommand();
    }
    else
    {
        System.Console.WriteLine("Неизвестный пункт меню. Попробуйте ещё раз.\n");
    }
}

logger.Close();

// здесь методы ShowMenu, CreateTask, ShowTasks, LoadTasks, ...
static void ShowMenu(UserSession session, Dictionary<string, ICommandHandler> commandHandlers)
{
    System.Console.WriteLine("===== Список задач =====");
    if (session.IsLoggedIn())
    {
        System.Console.WriteLine($"Пользователь: {session.CurrentUser!.Name}");
    }
    else
    {
        System.Console.WriteLine("Вы не вошли.");
    }

    foreach (var key in commandHandlers.Keys)
    {
        var handler = commandHandlers[key];
        System.Console.WriteLine($"{key}. {handler.MenuName}");
    }

    System.Console.WriteLine("0. Закрыть");
    System.Console.Write("Выберите пункт: ");
}