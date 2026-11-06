namespace ToDoApp.Users;

public class RegisterUserCommandHandler(IUserStorage userStorage, Logger logger)
{
    public UserResponse HandleCommand(RegisterUserCommand command)
    {
        string? name = command.Name?.Trim();

        if (string.IsNullOrEmpty(name) || name.Length > 100)
        {
            throw new ArgumentException("Имя должно содержать от 1 до 100 символов");
        }

        foreach(var u in userStorage.LoadUsers())
        {
            if(string.Equals(u.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Имя уже используется.");
            }
        }

        User user = new User{Name = name};

        userStorage.SaveUser(user);
        logger.LogInfo($"Зарегистрировалься пользователь {name}");

        return new UserResponse{
            Id = user.Id,
            Name = user.Name
        };
    }
}