using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

public class SearchTasksCommandHandler(ITaskStorage taskStorage, UserSession userSession)
{
    public List<TaskResponse> HandleCommand(string title)
    {
        
        var found = taskStorage.FindTask(title, userSession.CurrentUser!.Id);
        
        List<TaskResponse> taskResponses = new List<TaskResponse>();

        foreach(var i in found)
        {
            TaskResponse task = new TaskResponse();
            task.Id = i.Id;
            task.Title = i.Title;
            task.Done = i.Done;
            task.CreatedAt = i.CreatedAt;

            taskResponses.Add(task);
        }

        return taskResponses;
    }
}