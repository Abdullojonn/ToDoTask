namespace ToDoApp.CommandHandlers;
using ToDoApp;
using ToDoApp.CommandHandlers;
using System.Globalization;

public class DeleteTasksCommandHandler(IStorage storage, UserSession userSession, Logger logger): ICommandHandler
{
    public void HandleCommand()
    {
        if(!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите");
            return;
        }
        
        Console.Write("Введите номер задачи: ");
        string? input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            System.Console.WriteLine("Номер задачи не введен");
            return;
        }

        bool result = int.TryParse(input, out int taskId);
        if (!result)
        {
            System.Console.WriteLine("Неверный формат номера задачи");
            return;
        }

        result = storage.RemoveTask(taskId);
        if (!result)
        {
            System.Console.WriteLine("Такой задачи нет");
            return;
        }

        System.Console.WriteLine("Задача Успешно Удалено!\n");


        logger.LogInfo("Удалена задача");
    }
}