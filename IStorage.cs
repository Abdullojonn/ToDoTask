namespace ToDoApp;

public interface IStorage
{
    List<TodoTask> LoadTask();

    void SaveTask(TodoTask task);

    List<TodoTask> Find(string word);

    bool RemoveTask(int id);
}