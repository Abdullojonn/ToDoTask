using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;

namespace ToDoApp.Tasks;

public class EfTaskStorage(AppDbContext context) : ITaskStorage
{
    public async Task<TodoTask?> GetByIdAsync(int id)
    {
        return await context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
    }

    public async Task<List<TodoTask>> LoadTaskAsync()
    {
        return await context.Tasks.OrderBy(task => task.Id).ToListAsync();
    }

    public async Task SaveTaskAsync(TodoTask task)
    {
        context.Tasks.Add(task);

        await context.SaveChangesAsync();
    }

    public async Task<bool> RemoveTaskAsync(int id)
    {
        var task = await context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task == null)
            return false;

        context.Tasks.Remove(task);

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DoneAsync(int id)
    {
        var task = await context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task == null)
            return false;

        task.Done = true;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TodoTask>> FindTaskAsync(string title, int userId)
    {
        var tasks = await context.Tasks
            .Where(task => task.UserId == userId
            && EF.Functions.Like(task.Title, $"%{title}%"))
            .ToListAsync();

        return tasks;
    }

    public async Task<List<TodoTask>> LoadTaskByUserIdAsync(int userId)
    {
        return await context.Tasks
            .Where(task => task.UserId == userId)
            .OrderBy(task => task.Id)
            .ToListAsync();
    }

    public async Task<List<TodoTask>> SearchByDeadLineAsync(int userId, DateTime now)
    {
        return await context.Tasks
            .Where(task => task.UserId == userId
            && !task.Done
            && task.DeadLine < now)
            .ToListAsync();
    }
}
