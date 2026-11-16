using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users.Handlers;

public class LoginCommand()
{
    public string? Name {get; set;}
    public string? Password{get; set;}
}