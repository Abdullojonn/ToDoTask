using Microsoft.AspNetCore.Mvc;
using ToDoApp.Users.Handlers;

namespace ToDoApp.Users;

[ApiController]
[Route("api/users")]
public class UsersController(RegisterUserCommandHandler registerUserCommandHandler, LoginUserCommandHandler loginUserCommandHandler)
: ControllerBase
{
    [HttpPost("register")]
    public ActionResult<LoginResponse> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            var response = registerUserCommandHandler.HandleCommand(command);
            return Ok(response);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { error = exception.Message });
        }
    }

    [HttpPost("login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginCommand command)
    {
        try
        {
            var response = loginUserCommandHandler.HandleCommand(command);
            return Ok(response);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { error = exception.Message });
        }
    }
}
