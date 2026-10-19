using ToDoApp;
using ToDoApp.CommandHandlers;

// Dependency Injection


// handler -> storage -> File
// handler -> logger -> File


var logger = new Logger();
var storage = new Storage();
var userStorage = new UserStorage();
var session = new UserSession();

var createTaskHandler = new CreateTaskCommandHandler(storage, logger, session);
var showTasksHandler = new ShowTasksCommandHandler(storage, session);
var searchTasksHandler = new SearchTasksCommandHandler(storage, session);
var deleteTaskHandler = new DeleteTasksCommandHandler(storage, session, logger);
var doneTasksHandler = new DoneTasksCommandHandler(storage, session);

var registerHandler = new RegisterUserCommandHandler(userStorage, logger);
var loginHandler = new LoginCommandHandler(userStorage, session, logger);
var logoutHandler = new LogoutCommandHandler(session, logger);


Dictionary<string, ICommandHandler> commandHandlers = new Dictionary<string, ICommandHandler>
{
    { "1", createTaskHandler },
    { "2", showTasksHandler },
    { "3", searchTasksHandler },
    { "4", deleteTaskHandler },
    { "5", doneTasksHandler},
    { "6", registerHandler},
    { "7", loginHandler},
    { "8", logoutHandler}
};


while (true)
{

    ShowMenu(session);
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
static void ShowMenu(UserSession session)
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

    System.Console.WriteLine("1. Создать задачу");
    System.Console.WriteLine("2. Показать список задач");
    System.Console.WriteLine("3. Найти задачу");
    System.Console.WriteLine("4. Удалить задачу");
    System.Console.WriteLine("5. Помечать задачу");
    System.Console.WriteLine("6. Зарегистрироваться");
    System.Console.WriteLine("7. Войти");
    System.Console.WriteLine("8. Выйти из аккаунта");
    System.Console.WriteLine("0. Выход");
    System.Console.Write("Выберите пункт: ");
}
