namespace ToDoApp;

public interface ITaskStorage
{
    List<TodoTask> LoadTask();

    void SaveTask(TodoTask task);

    bool RemoveTask(int id, int userId);
}