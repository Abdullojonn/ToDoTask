using ToDoApp.Users;

namespace ToDoApp;

public class CreateTaskCommandHandler(ITaskStorage storage, Logger logger, UserSession userSession)
{
    public TaskResponse HandleCommand(CreateTaskCommand createTaAskCommand)
    {

        //if(!userSession.IsLoggedIn())
        

        string title = createTaAskCommand.Title.Trim();

        if(string.IsNullOrWhiteSpace(title) || title.Length > 200)
        {
            throw new ArgumentException("Название задачи должно содержать от 1 до 200 символов");
        }

        TodoTask task = new TodoTask();
        task.Title = title;
        task.CreatedAt = DateTime.Now;
        task.UserId = userSession.CurrentUser!.Id;

        storage.SaveTask(task);
        logger.LogInfo($"Создана задача {task.Id} от пользователья {userSession.CurrentUser.Name}");

        return new TaskResponse
        {
            Id = task.Id
        };
    }
}