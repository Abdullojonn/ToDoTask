using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class GetTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public List<TaskResponse> Get()
    {
        List<TodoTask> tasks = taskStorage.LoadTaskByUserId(userContext.GetRequiredUserId());

        var taskResponses = tasks
            .Select(task => task.MapToResponse())
            .ToList();

        var taskResponses2 = tasks
            .Select(task => TaskMapping.MapToResponse(task))
            .ToList();

        return taskResponses;
    }
}