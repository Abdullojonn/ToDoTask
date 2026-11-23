using Microsoft.Data.Sqlite;

namespace ToDoApp;

public class Database(string connectionString)
{
    // Каждый вызов — новое открытое соединение.
    // Тот, кто его получил, обязан закрыть его через using.
    public async Task<SqliteConnection> OpenConnectionAsync()
    {
        var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }

    // Создаёт таблицы, если их ещё нет. Вызывается один раз при старте программы.
    public async Task EnsureCreatedAsync()
    {
        using var connection = await OpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Users (
                Id   INTEGER PRIMARY KEY,
                Name TEXT NOT NULL UNIQUE,
                Password TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Tasks(
                Id INTEGER PRIMARY KEY,
                Title TEXT NOT NULL,
                CreatedAt TEXT NOT NULL,
                Done INTEGER NOT NULL DEFAULT 0,
                UserId INTEGER NOT NULL,
                DeadLine TEXT NOT NULL
            );

            """;
         await command.ExecuteNonQueryAsync();
    }
}
