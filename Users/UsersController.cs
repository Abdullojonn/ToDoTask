using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

[ApiController]
[Route("api/users")]
public class UsersController(RegisterUserCommandHandler registerUserCommandHandler, LoginUserCommandHandler loginUserCommandHandler)
: ControllerBase
{
    [HttpPost("register")]
    public ActionResult<UserResponse> Register([FromBody] RegisterUserCommand command)
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
    public ActionResult<UserResponse> Login([FromBody] LoginCommand command)
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
