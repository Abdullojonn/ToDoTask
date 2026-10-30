using Microsoft.Data.Sqlite;

namespace ToDoApp;

public class Database(string connectionString)
{
    // Каждый вызов — новое открытое соединение.
    // Тот, кто его получил, обязан закрыть его через using.
    public SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection(connectionString);
        connection.Open();
        return connection;
    }

    // Создаёт таблицы, если их ещё нет. Вызывается один раз при старте программы.
    public void EnsureCreated()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();

        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Users (
                Id   INTEGER PRIMARY KEY,
                Name TEXT    NOT NULL UNIQUE
            );

            CREATE TABLE IF NOT EXISTS Tasks(
                Id INTEGER PRIMARY KEY,
                Title TEXT NOT NULL,
                CreatedAt TEXT NOT NULL,
                Done INTEGER NOT NULL DEFAULT 0,
                UserId INTEGER NOT NULL
            );

            """;
        command.ExecuteNonQuery();
    }
}
