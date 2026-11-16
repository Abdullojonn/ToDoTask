using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Tasks.Handlers;

public class CreateTaskCommand()
{
    public string Title {get; set;} = string.Empty;
    public DateTime DeadLine {get; set;}
}