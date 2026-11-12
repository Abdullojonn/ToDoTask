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
services.AddTransient<SearchTasksCommandHandler>();

services.AddSingleton<UserSession>();

var app = builder.Build();
app.Services.GetRequiredService<Database>().EnsureCreated();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
