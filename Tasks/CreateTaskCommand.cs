using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

public class CreateTaskCommand()
{
    public string Title {get; set;} = string.Empty;
}