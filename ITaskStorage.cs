using System.Reflection.Metadata;

namespace ToDoApp;

public interface ITaskStorage
{
    List<TodoTask> LoadTask();

    void SaveTask(TodoTask task);

    bool RemoveTask(int id, int userId);

    bool Done(int id, int userId);
}