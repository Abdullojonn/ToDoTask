using Microsoft.AspNetCore.Mvc;
using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class SearchTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public async Task<List<TaskResponse>> HandleCommand(string title)
    {
        
        var found = await taskStorage.FindTaskAsync(title, userContext.GetRequiredUserId());
        
        var responses = found
            .Select(task => TaskMapping.MapToResponse(task))
            .ToList();

        return responses;
    }
}