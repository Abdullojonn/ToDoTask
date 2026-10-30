namespace ToDoApp.CommandHandlers;
using ToDoApp;
using ToDoApp.CommandHandlers;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;

public class DoneTasksCommandHandler(ITaskStorage storage, UserSession userSession) : ICommandHandler
{
    public string MenuName => "Отметить задачу как выполненную";

    public void HandleCommand()
    {
        if(!userSession.IsLoggedIn())
        {
            System.Console.WriteLine("Сначала войдите");
            return;
        }

        System.Console.Write("Введите номер задачи: ");
        string? input = Console.ReadLine();

        List<TodoTask> tasks = storage.LoadTask();

        if(tasks.Count <= 0){
            System.Console.WriteLine("Список задач пуст");
            return;
        }

        var result = int.TryParse(input, out int taskId);
        if (!result)
        {
            System.Console.WriteLine("Введите число");
            return;
        }

        bool done = storage.Done(taskId, userSession.CurrentUser!.Id);

        if (!done)
        {
            System.Console.WriteLine("Такой Задачи Нет");
            return;
        }
        
        System.Console.WriteLine("Задача Успешно Помечена.\n");
    }
}