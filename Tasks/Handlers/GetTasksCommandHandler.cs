using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class GetTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public async Task<List<TaskResponse>> Get()
    {
        List<TodoTask> tasks = await taskStorage.LoadTaskByUserIdAsync(userContext.GetRequiredUserId());

        var responses = tasks
            .Select(task => TaskMapping.MapToResponse(task))
            .ToList();

        return responses;
    }
}