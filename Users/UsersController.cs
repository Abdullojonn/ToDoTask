using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

[ApiController]
[Route("api/users")]
public class UserController(RegisterUserCommand registerUserCommand) : ControllerBase
{
    
}