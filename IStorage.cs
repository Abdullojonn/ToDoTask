namespace ToDoApp;

public interface IStorage
{
    List<TodoTask> LoadTask();

    void SaveTask(TodoTask task);

    bool RemoveTask(int id);
}