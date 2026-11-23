using System.Reflection.Metadata;

namespace ToDoApp.Tasks;

public interface ITaskStorage
{
    Task<TodoTask?> GetByIdAsync(int id);

    Task<List<TodoTask>> LoadTaskAsync();

    Task SaveTaskAsync(TodoTask task);

    Task<bool> RemoveTaskAsync(int id);

    Task<bool> DoneAsync(int id);

    Task<List<TodoTask>> FindTaskAsync(string title, int userId);

    Task<List<TodoTask>> LoadTaskByUserIdAsync(int userId);

    Task<List<TodoTask>> SearchByDeadLineAsync(int userId, DateTime now);
}