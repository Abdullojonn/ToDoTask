namespace ToDoApp.Tasks;

public class TodoTask
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public DateTime CreatedAt { get; set; }

    public DateTime DeadLine { get; set; }

    public bool Done { get; set; }

    public int UserId { get; set;}

    public int Usersf;
}
