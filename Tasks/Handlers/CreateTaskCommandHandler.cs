using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class CreateTaskCommandHandler(ITaskStorage storage, Logger logger, ICurrentUserContext userContext)
{
    public TaskResponse HandleCommand(CreateTaskCommand createTaAskCommand)
    {
        string title = createTaAskCommand.Title.Trim();

        if(string.IsNullOrWhiteSpace(title) || title.Length > 200)
        {
            throw new ArgumentException("Название задачи должно содержать от 1 до 200 символов");
        }

        TodoTask task = new TodoTask();
        task.Title = title;
        task.CreatedAt = DateTime.Now;
        task.UserId = userContext.GetRequiredUserId();

        DateTime deadline = createTaAskCommand.DeadLine;

        if(deadline < DateTime.Now)
        {
            throw new ArgumentException("Дедлайн не может быть в прошлом");
        }

        task.DeadLine = deadline;

        storage.SaveTask(task);
        logger.LogInfo($"Создана задача {task.Id} от пользователья {userContext.GetRequiredUserId()}");

        return new TaskResponse
        {
            Id = task.Id
        };
    }
}