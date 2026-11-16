namespace ToDoApp.Tasks;

public static class TaskMapping
{
    public static TaskResponse MapToResponse(this TodoTask task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Done = task.Done,
            CreatedAt = task.CreatedAt,
            DeadLine = task.DeadLine
        };
    }
}
