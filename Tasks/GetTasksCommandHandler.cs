using ToDoApp.Users;

namespace ToDoApp;

public class GetTasksCommandHandler(ITaskStorage taskStorage, UserSession userSession)
{
    public List<TaskResponse> Get()
    {
        int id = userSession.CurrentUser!.Id;

        List<TodoTask> tasks = taskStorage.LoadTaskByUserId(id);

        List<TaskResponse> taskResponses = new List<TaskResponse>();

        foreach(var i in tasks)
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