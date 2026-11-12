namespace ToDoApp.Users;

public class LoginUserCommandHandler(IUserStorage userStorage, Logger logger, UserSession userSession)
{
    public UserResponse HandleCommand(LoginCommand command)
    {
        string? name = command.Name?.Trim();

        if (string.IsNullOrEmpty(name) || name.Length > 100)
        {
            throw new ArgumentException("Имя должно содержать от 1 до 100 символов");
        }

        User? found = userStorage.FindUserByName(name);

        if (found == null)
        {
            throw new ArgumentException("Такого имени нет");
        }

        if(command.Password != found.Password)
        {
            throw new ArgumentException("Пароль неверный.");
        }

        userSession.CurrentUser = found;
        logger.LogInfo($"Зашёл пользователь {userSession.CurrentUser.Name}");
        
        return new UserResponse
        {
            Id = found.Id,
            Name = found.Name!  
        };
    }
}