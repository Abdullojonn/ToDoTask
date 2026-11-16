namespace ToDoApp.Tasks;

public class TaskResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool Done { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeadLine { get; set; }
}