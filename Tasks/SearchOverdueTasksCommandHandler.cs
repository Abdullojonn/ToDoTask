using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

public class SearchOverdueTasksCommandHandler(ITaskStorage taskStorage, UserSession userSession)
{
    public List<TaskResponse> HandleCommand(DateTime now)
    {
        
        var found = taskStorage.SearchByDeadLine(userSession.CurrentUser!.Id, now);
        
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