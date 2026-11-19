using Microsoft.AspNetCore.Mvc;
using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class SearchTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public List<TaskResponse> HandleCommand(string title)
    {
        
        var found = taskStorage.FindTask(title, userContext.GetRequiredUserId());
        
        var responses = found
            .Select(task => TaskMapping.MapToResponse(task))
            .ToList();

        return responses;
    }
}