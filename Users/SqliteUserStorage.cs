using ToDoApp;
using ToDoApp.Users;

// Хранит пользователей в SQLite.
// Реализует тот же IUserStorage, что и UserStorage (CSV), поэтому обработчики не меняются.
public class SQLiteUserStorage(Database database) : IUserStorage
{
    public async Task<List<User>> LoadUsersAsync()
    {
        List<User> users = new List<User>();

        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name FROM Users ORDER BY Id";

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            User user = new User();
            user.Id = reader.GetInt32(0);
            user.Name = reader.GetString(1);
            users.Add(user);
        }

        return users;
    }

    public async Task SaveUserAsync(User user)
    {
        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();

        // Id не передаём: база назначит его сама и вернёт через RETURNING.
        // Имя передаём ПАРАМЕТРОМ ($name), а не вклеиваем в строку — так нельзя сделать SQL-инъекцию.
        command.CommandText = "INSERT INTO Users (Name, Password) VALUES ($name, $password) RETURNING Id";
        command.Parameters.AddWithValue("$name", user.Name);
        command.Parameters.AddWithValue("$password", user.Password);

        user.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task<User?> FindUserByNameAsync(string name)
    {
        using var connection = await database.OpenConnectionAsync();
        using var command = connection.CreateCommand();

        command.CommandText = "SELECT Id, Name, Password FROM Users WHERE Name = $name";
        command.Parameters.AddWithValue("$name", name);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            User user = new User();
            user.Id = reader.GetInt32(0);
            user.Name = reader.GetString(1);
            user.Password = reader.GetString(2);
            return user;
        }
        return null;
    }
}
