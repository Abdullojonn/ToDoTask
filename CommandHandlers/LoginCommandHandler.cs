using ToDoApp;
using ToDoApp.CommandHandlers;

public class LoginCommandHandler(IUserStorage userStorage, UserSession session, Logger logger) : ICommandHandler
{
    public string MenuName => "Войти";

    public void HandleCommand()
    {
        System.Console.Write("Введите Имя: ");
        string? name = Console.ReadLine();

        User? found = null;
        
        List<User> users = userStorage.LoadUsers();

        foreach(var user in users){
            if(string.Equals(user.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                found = user;
                break;
            }
        }

        if (found == null)
        {
            Console.WriteLine("Пользователь не найден.");
            logger.LogWarning($"Неудачная Попытка входа: {name}");
            return;
        }

        session.CurrentUser = found;
        Console.WriteLine($"Добро пожаловать, {found.Name}!");
        logger.LogInfo($"{found.Name} вошёл в систему");
    }
}