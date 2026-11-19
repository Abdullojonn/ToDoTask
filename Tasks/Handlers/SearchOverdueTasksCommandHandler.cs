using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class SearchOverdueTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public List<TaskResponse> HandleCommand(DateTime now)
    {
        
        var found = taskStorage.SearchByDeadLine(userContext.GetRequiredUserId(), now);

        var responses = found
            .Select(task => TaskMapping.MapToResponse(task))
            .ToList();

        return responses;
    }
}