using System.Reflection.Metadata;

namespace ToDoApp.Tasks;

public interface ITaskStorage
{
    Task<TodoTask?> GetById(int id);

    Task<List<TodoTask>> LoadTask();

    void SaveTask(TodoTask task);

    bool RemoveTask(int id);

    bool Done(int id);

    List<TodoTask> FindTask(string title, int userId);

    List<TodoTask> LoadTaskByUserId(int userId);

    List<TodoTask> SearchByDeadLine(int userId, DateTime now);
}