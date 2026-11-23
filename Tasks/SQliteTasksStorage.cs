namespace ToDoApp.Tasks;

public class SQliteTasksStorage(Database database) : ITaskStorage
{
    public async Task<TodoTask?> GetByIdAsync(int id)
    {
        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Title, CreatedAt, Done, UserId, DeadLine FROM Tasks WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            TodoTask task = new TodoTask();
            task.Id = reader.GetInt32(0);
            task.Title = reader.GetString(1);
            task.CreatedAt = DateTime.Parse(reader.GetString(2));
            task.Done = reader.GetInt32(3) == 1;
            task.UserId = reader.GetInt32(4);
            task.DeadLine = DateTime.Parse(reader.GetString(5));
            return task;
        }

        return null;
    }

    public async Task<List<TodoTask>> LoadTaskAsync()
    {
        List<TodoTask> tasks = new List<TodoTask>();

        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Title, CreatedAt, Done, UserId, DeadLine FROM Tasks ORDER BY Id";

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            TodoTask task = new TodoTask();
            task.Id = reader.GetInt32(0);
            task.Title = reader.GetString(1);
            task.CreatedAt = DateTime.Parse(reader.GetString(2));
            task.Done = reader.GetInt32(3) == 1;
            task.UserId = reader.GetInt32(4);
            task.DeadLine = DateTime.Parse(reader.GetString(5));
            tasks.Add(task);
        }

        return tasks;
    }

    public async Task SaveTaskAsync(TodoTask task)
    {
        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO Tasks (Title, CreatedAt, DeadLine, Done, UserId) 
                                VALUES ($title, $createdAt, $deadline, $done, $userId)
                                RETURNING Id";

        command.Parameters.AddWithValue("$title", task.Title);
        command.Parameters.AddWithValue("$createdAt", task.CreatedAt.ToString("yyyy-MM-dd HH:mm"));
        command.Parameters.AddWithValue("$deadline", task.DeadLine.ToString("yyyy-MM-dd HH:mm"));
        command.Parameters.AddWithValue("$done", task.Done ? 1 : 0);
        command.Parameters.AddWithValue("$userId", task.UserId);

        task.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task<bool> RemoveTaskAsync(int id)
    {
        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM Tasks Where Id = $id";
        command.Parameters.AddWithValue("$id", id);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DoneAsync(int id)
    {
        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = "UPDATE Tasks SET DONE = 1 Where Id = $id";
        command.Parameters.AddWithValue("$id", id);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<List<TodoTask>> FindTaskAsync(string title, int userId)
    {
        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = "SELECT Id, Title, CreatedAt, Done, UserId, DeadLine FROM Tasks WHERE Title LIKE $title AND UserId = $userId";

        command.Parameters.AddWithValue("$title", $"%{title}%");
        command.Parameters.AddWithValue("$userId", userId);

        using var reader = await command.ExecuteReaderAsync();
        List<TodoTask> tasks = new List<TodoTask>();
        while (await reader.ReadAsync())
        {
            TodoTask task = new TodoTask();
            task.Id = reader.GetInt32(0);
            task.Title = reader.GetString(1);
            task.CreatedAt = DateTime.Parse(reader.GetString(2));
            task.Done = reader.GetInt32(3) == 1;
            task.UserId = reader.GetInt32(4);
            task.DeadLine = DateTime.Parse(reader.GetString(5));
            tasks.Add(task);
        }

        return tasks;
    }

    public async Task<List<TodoTask>> LoadTaskByUserIdAsync(int userId)
    {
        List<TodoTask> tasks = new List<TodoTask>();

        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = "SELECT id, title, createdAt, done, userId, deadLine FROM Tasks WHERE UserId = $userId";

        command.Parameters.AddWithValue("$userId", userId);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            TodoTask task = new TodoTask();
            task.Id = reader.GetInt32(0);
            task.Title = reader.GetString(1);
            task.CreatedAt = DateTime.Parse(reader.GetString(2));
            task.Done = reader.GetInt32(3) == 1;
            task.UserId = reader.GetInt32(4);
            task.DeadLine = DateTime.Parse(reader.GetString(5));
            tasks.Add(task);
        }

        return tasks;
    }

    public async Task<List<TodoTask>> SearchByDeadLineAsync(int userId, DateTime now)
    {
        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = "SELECT Id, Title, CreatedAt, Done, UserId, DeadLine FROM Tasks WHERE UserId = $userId AND DONE = 0 AND DeadLine < $now";

        command.Parameters.AddWithValue("$userId", userId);
        command.Parameters.AddWithValue("$now", now.ToString("yyyy-MM-dd HH:mm"));

        using var reader = await command.ExecuteReaderAsync();
        List<TodoTask> tasks = new List<TodoTask>();
        while (await reader.ReadAsync())
        {
            TodoTask task = new TodoTask();
            task.Id = reader.GetInt32(0);
            task.Title = reader.GetString(1);
            task.CreatedAt = DateTime.Parse(reader.GetString(2));
            task.Done = reader.GetInt32(3) == 1;
            task.UserId = reader.GetInt32(4);
            task.DeadLine = DateTime.Parse(reader.GetString(5));
            tasks.Add(task);
        }

        return tasks;
    }

}
