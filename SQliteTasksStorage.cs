using System.Data;
using System.Runtime.CompilerServices;
using ToDoApp;


public class SQliteTasksStorage(Database database) : ITaskStorage
{
    public List<TodoTask>  LoadTask()
    {
        List<TodoTask> tasks = new List<TodoTask>();

        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Title, CreatedAt, Done, UserId FROM Users ORDER BY Id";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            TodoTask task = new TodoTask();
            task.Id = reader.GetInt32(0);
            task.Title = reader.GetString(1);
            task.CreatedAt = DateTime.Parse(reader.GetString(2));
            task.Done = reader.GetInt32(3) == 1;
            task.UserId = reader.GetInt32(4);
            tasks.Add(task);
        }

        return tasks;
    }

    public void SaveTask(TodoTask task)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO Task (Title, CreatedAt, Done, UserId) 
                                VALUES ($title, $createdAt, $done, $userId)
                                RETURNING Id";
        
        command.Parameters.AddWithValue("$title", task.Title);
        command.Parameters.AddWithValue("$createdAt", task.CreatedAt.ToString("yyyy-MM-dd HH:mm"));
        command.Parameters.AddWithValue("$done", task.Done);
        command.Parameters.AddWithValue("$userId", task.UserId);

        task.Id = Convert.ToInt32(command.ExecuteScalar());
    }

    public bool RemoveTask(int id, int userId)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM Tasks Where Id = $id AND UserId = $userId";
        
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$userId", userId);

        return command.ExecuteNonQuery() > 0;
    }

    public bool Done(int id, int userId)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();

        command.CommandText = "UPDATE Tasks SET DONE = 1 Where Id = $id AND UserId = $userId";

        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$userId", userId);

        return command.ExecuteNonQuery() > 0;
    }
}