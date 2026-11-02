using ToDoApp.Users;

public record RegisterUserCommand(string Name);

public class RegisterUserCommandHandler(IUserStorage userStorage, Logger logger)
{
    public void HandleCommand(RegisterUserCommand command)
    {
        string name = command.Name;
        if (name == "" || name == null || name.Contains(",") || name.Contains(".") || name.Contains("!"))
        {
            throw new ArgumentException("Имя не может быть пустым и не должно содержать запрещенные символы.");
        }

        User? user = userStorage.FindUserByName(name);

        if (user != null)
        {
            logger.LogInfo($"Попытка зарегистрировать уже существующего пользователя {name}");
            throw new ArgumentException("Имя уже используется.");
        }

        User u = new User { Name = name };

        userStorage.SaveUser(u);

        logger.LogInfo($"Зарегистрирован пользователь {name}");
    }
}