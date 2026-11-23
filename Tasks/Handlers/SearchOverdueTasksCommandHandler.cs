using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class SearchOverdueTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public async Task<List<TaskResponse>> HandleCommand(DateTime now)
    {
        
        var found = await taskStorage.SearchByDeadLineAsync(userContext.GetRequiredUserId(), now);

        var responses = found
            .Select(task => TaskMapping.MapToResponse(task))
            .ToList();

        return responses;
    }
}