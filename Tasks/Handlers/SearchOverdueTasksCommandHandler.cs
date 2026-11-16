using Microsoft.AspNetCore.Mvc;
using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class SearchOverdueTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public List<TaskResponse> HandleCommand(DateTime now)
    {
        
        var found = taskStorage.SearchByDeadLine(userContext.GetRequiredUserId(), now);
        
        List<TaskResponse> taskResponses = new List<TaskResponse>();

        foreach(var i in found)
        {
            TaskResponse task = new TaskResponse();
            task.Id = i.Id;
            task.Title = i.Title;
            task.Done = i.Done;
            task.CreatedAt = i.CreatedAt;
            task.DeadLine = i.DeadLine;

            taskResponses.Add(task);
        }

        return taskResponses;
    }
}