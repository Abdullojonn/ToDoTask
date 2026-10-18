using ToDoApp;
using ToDoApp.CommandHandlers;

public class RegisterUserCommandHandler(IUserStorage userStorage, Logger logger) : ICommandHandler
{
    public void HandleCommand()
    {
        System.Console.Write("Введите имя: ");
        string? name = Console.ReadLine();

        if(name == "" || name == null || name.Contains(",") || name.Contains(".") || name.Contains("!"))
        {
            System.Console.WriteLine("Некоректно.");
            return;
        }

        List<User> users = userStorage.LoadUsers();

        foreach(var user in users)
        {
            if(name.ToLower() == user.Name!.ToLower())
            {
                System.Console.WriteLine("Пользователь с таким именем уже есть.");
                return;
            }
        }
        User u = new User { Name = name};
        userStorage.SaveUser(u);
        
        System.Console.WriteLine($"Пользователь {name} зарегистрирован.\n");
        logger.LogInfo($"Зарегистрирован пользователь {name}");
    }
}