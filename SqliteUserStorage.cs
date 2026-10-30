using ToDoApp;

// Хранит пользователей в SQLite.
// Реализует тот же IUserStorage, что и UserStorage (CSV), поэтому обработчики не меняются.
public class SqliteUserStorage(Database database) : IUserStorage
{
    public List<User> LoadUsers()
    {
        List<User> users = new List<User>();

        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, CreatedAt, Done, UserId FROM Users ORDER BY Id";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            User user = new User();
            user.Id = reader.GetInt32(0);
            user.Name = reader.GetString(1);
            users.Add(user);
        }

        return users;
    }

    public void SaveUser(User user)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();

        // Id не передаём: база назначит его сама и вернёт через RETURNING.
        // Имя передаём ПАРАМЕТРОМ ($name), а не вклеиваем в строку — так нельзя сделать SQL-инъекцию.
        command.CommandText = "INSERT INTO Users (Name) VALUES ($name) RETURNING Id";
        command.Parameters.AddWithValue("$name", user.Name);

        user.Id = Convert.ToInt32(command.ExecuteScalar());
    }
}
