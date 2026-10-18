using ToDoApp.CommandHandlers;

public class LogoutCommandHandler(UserSession userSession, Logger logger) : ICommandHandler
{
    public void HandleCommand()
    {
        if (!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Некто не вошёл.");
            return;
        }

        string? name = userSession.CurrentUser!.Name;

        userSession.CurrentUser = null;

        System.Console.WriteLine("Вы вышли из аккаунта");
        logger.LogInfo($"{name} ВЫШЕЛ.");
    }
}