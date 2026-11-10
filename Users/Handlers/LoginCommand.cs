using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

public class LoginCommand()
{
    public string? Name {get; set;}
}