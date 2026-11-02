using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

[ApiController]
[Route("api/users")]
public class UsersController(RegisterUserCommandHandler registerUserCommandHandler) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            registerUserCommandHandler.HandleCommand(command);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            throw; // 500
        }

        return Ok();
    }
}

