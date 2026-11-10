using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Users;

[ApiController]
[Route("api/tasks")]
public class TasksController(CreateTaskCommandHandler createTaskCommandHandler, GetTasksCommandHandler getTasksCommandHandler, DeleteTaskCommandHandler deleteTaskCommandHandler, DoneTaskCommandHandler doneTaskCommandHandler): ControllerBase
{
    [HttpPost("create")]
    public ActionResult<TaskResponse> Create([FromBody] CreateTaskCommand command)
    {
        try
        {
            var response = createTaskCommandHandler.HandleCommand(command);
            return response;
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

    [HttpGet("get")]
    public ActionResult<List<TaskResponse>> Get()
    {
        try
        {
            var tasks = getTasksCommandHandler.Get();
            return Ok(tasks);
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

    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            bool delete = deleteTaskCommandHandler.DeleteTask(id);
            if (!delete)
            {
                return NotFound();
            }
            
            return Ok();
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

    [HttpPatch("done/{id}")]
    public ActionResult Done(int id)
    {
        try
        {
            bool done = doneTaskCommandHandler.DoneTask(id);
            if (!done)
            {
                return NotFound();
            }
            
            return Ok();
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
