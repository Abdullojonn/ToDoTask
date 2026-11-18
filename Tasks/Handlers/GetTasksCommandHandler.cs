using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class GetTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public List<TaskResponse> Get()
    {
        List<TodoTask> tasks = taskStorage.LoadTaskByUserId(userContext.GetRequiredUserId());

        var responses = tasks
            .Select(task => TaskMapping.MapToResponse(task))
            .ToList();

        return responses;
    }
}