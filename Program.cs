using System.Reflection.Metadata;
using ToDoApp;
using ToDoApp.Users;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddControllers();

services.AddSingleton(new Database("Data Source=todo.db"));
services.AddSingleton<Logger>();

services.AddTransient<ITaskStorage, SQliteTasksStorage>();
services.AddTransient<IUserStorage, SqliteUserStorage>();


services.AddTransient<RegisterUserCommandHandler>();
services.AddTransient<CreateTaskCommandHandler>();
services.AddTransient<GetTasksCommandHandler>();
services.AddTransient<DeleteTaskCommandHandler>();
services.AddTransient<DoneTaskCommandHandler>();
services.AddTransient<LoginUserCommandHandler>();

services.AddSingleton<UserSession>();
//services.AddTransient<ICommandHandler, LoginCommandHandler>();
//services.AddTransient<ICommandHandler, LogoutCommandHandler>();

var app = builder.Build();
app.Services.GetRequiredService<Database>().EnsureCreated();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();


//// DB
//// async await
//// web -> rest api
//// docker -> postgres
//// parallel programming

//// Dependency Injection
//var services = new ServiceCollection();

//services.AddSingleton<Logger>();
//services.AddSingleton<UserSession>();

//services.AddSingleton(new Database("Data Source=todo.db"));

//services.AddTransient<ITaskStorage, SQliteTasksStorage>();
//services.AddTransient<IUserStorage, SqliteUserStorage>();

//services.AddTransient<ICommandHandler, CreateTaskCommandHandler>();
//services.AddTransient<ICommandHandler, ShowTasksCommandHandler>();
//services.AddTransient<ICommandHandler, SearchTasksCommandHandler>();
//services.AddTransient<ICommandHandler, DeleteTasksCommandHandler>();
//services.AddTransient<ICommandHandler, DoneTasksCommandHandler>();
//services.AddTransient<ICommandHandler, MoveTaskCommandHandler>();
//services.AddTransient<ICommandHandler, StatisticsTaskCommandHandler>();

//services.AddTransient<ICommandHandler, RegisterUserCommandHandler>();
//services.AddTransient<ICommandHandler, LoginCommandHandler>();
//services.AddTransient<ICommandHandler, LogoutCommandHandler>();

//var provider = services.BuildServiceProvider();

//// Создать таблицы, если базы ещё нет
//provider.GetRequiredService<Database>().EnsureCreated();

//var logger = provider.GetRequiredService<Logger>();
//var session = provider.GetRequiredService<UserSession>();


//var handlers =  provider.GetServices<ICommandHandler>();

//Dictionary<string, ICommandHandler> commandHandlers = new Dictionary<string, ICommandHandler>();

//for (int i = 0; i < handlers.Count(); i++)
//{
//    commandHandlers.Add((i + 1).ToString(), handlers.ElementAt(i));
//}


//while (true)
//{

//    ShowMenu(session, commandHandlers);
//    string? choice = Console.ReadLine();

// // в зависимости от choice вызвать CreateTask(), ShowTasks() или выйти

//    if(choice == "0")
//    {
//        break;
//    }
//    if (string.IsNullOrEmpty(choice))
//    {
//        System.Console.WriteLine("Пункт меню не выбран. Попробуйте ещё раз.\n");
//        break;
//    }

//    if (commandHandlers.ContainsKey(choice))
//    {
//        Console.Clear();

//        commandHandlers[choice].HandleCommand();
//    }
//    else
//    {
//        System.Console.WriteLine("Неизвестный пункт меню. Попробуйте ещё раз.\n");
//    }
//}

//logger.Close();

//// здесь методы ShowMenu, CreateTask, ShowTasks, LoadTasks, ...
//static void ShowMenu(UserSession session, Dictionary<string, ICommandHandler> commandHandlers)
//{
//    System.Console.WriteLine("===== Список задач =====");
//    if (session.IsLoggedIn())
//    {
//        System.Console.WriteLine($"Пользователь: {session.CurrentUser!.Name}");
//    }
//    else
//    {
//        System.Console.WriteLine("Вы не вошли.");
//    }

//    foreach (var key in commandHandlers.Keys)
//    {
//        var handler = commandHandlers[key];
//        System.Console.WriteLine($"{key}. {handler.MenuName}");
//    }

//    System.Console.WriteLine("0. Закрыть");
//    System.Console.Write("Выберите пункт: ");
//}
