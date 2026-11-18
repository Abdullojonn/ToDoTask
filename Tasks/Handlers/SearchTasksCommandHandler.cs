using Microsoft.AspNetCore.Mvc;
using ToDoApp.Users.IdAcess;

namespace ToDoApp.Tasks.Handlers;

public class SearchTasksCommandHandler(ITaskStorage taskStorage, ICurrentUserContext userContext)
{
    public List<TaskResponse> HandleCommand(string title)
    {
        
        var found = taskStorage.FindTask(title, userContext.GetRequiredUserId());
        
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