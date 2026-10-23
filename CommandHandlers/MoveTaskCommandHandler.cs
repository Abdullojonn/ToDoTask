namespace ToDoApp.CommandHandlers;
using ToDoApp;
using ToDoApp.CommandHandlers;
using System.Globalization;
using System.Runtime.CompilerServices;

public class MoveTaskCommandHandler(ITaskStorage storage, UserSession userSession, Logger logger) : ICommandHandler
{
    public string MenuName => "Пердать задачу";

    public void HandleCommand()
    {
        logger.LogError("adsad");

        if (!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите");
            return;
        }

        System.Console.Write("Введите номер задачи: ");
        string? input = Console.ReadLine();

    }
}