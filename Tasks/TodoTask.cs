namespace ToDoApp.Tasks;

public class TodoTask
{
    public int Id;
    public string? Title;
    public DateTime CreatedAt;
    public DateTime DeadLine;
    public bool Done = false;
    public int UserId;
}
