using ToDoApp;
using ToDoApp.CommandHandlers;

public class RegisterUserCommandHandler(IUserStorage userStorage, Logger logger) : ICommandHandler
{
    public string MenuName => "Зарегистрировать пользователя";
    public void HandleCommand()
    {
        System.Console.Write("Введите имя: ");
        string? name = Console.ReadLine();

        if(name == "" || name == null || name.Contains(",") || name.Contains(".") || name.Contains("!"))
        {
            System.Console.WriteLine("Некоректно.");
            return;
        }

        User? user = userStorage.FindUserByName(name);
        
        if(user != null)
        {
            System.Console.WriteLine($"Пользователь {name} уже зарегистрирован.\n");
            logger.LogInfo($"Попытка зарегистрировать уже существующего пользователя {name}");
            return;
        }
        else
        {
            System.Console.WriteLine($"Пользователь {name} создан.\n");
        }

        User u = new User{Name = name};
        userStorage.SaveUser(u);
        
        System.Console.WriteLine($"Пользователь {name} зарегистрирован.\n");
        logger.LogInfo($"Зарегистрирован пользователь {name}");
    }
}