using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

public class LoginCommand()
{
    public string? Name {get; set;}
    public string? Password{get; set;}
}