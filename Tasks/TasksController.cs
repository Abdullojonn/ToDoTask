using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Tasks.Handlers;

namespace ToDoApp.Tasks;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TasksController(
    CreateTaskCommandHandler createTaskCommandHandler,
    GetTasksCommandHandler getTasksCommandHandler,
    DeleteTaskCommandHandler deleteTaskCommandHandler,
    DoneTaskCommandHandler doneTaskCommandHandler,
    SearchTasksCommandHandler searchTasksCommandHandler,
    SearchOverdueTasksCommandHandler searchOverdueTasksCommandHandler) : ControllerBase
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
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            bool delete = await deleteTaskCommandHandler.DeleteTask(id);
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
    public async Task<ActionResult> Done(int id)
    {
        try
        {
            bool done = await doneTaskCommandHandler.DoneTask(id);
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

    [HttpGet("search")]
    public ActionResult Search(string title)
    {
        try
        {
            var found = searchTasksCommandHandler.HandleCommand(title);

            return Ok(found);
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

    [HttpGet("overdue")]
    public ActionResult Overdue()
    {
        try
        {
            var found = searchOverdueTasksCommandHandler.HandleCommand(DateTime.Now);

            return Ok(found);
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
