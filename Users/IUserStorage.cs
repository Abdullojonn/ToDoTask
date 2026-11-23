using ToDoApp;
using ToDoApp.Users;

public interface IUserStorage
{
    Task SaveUserAsync(User user);
    Task<User?> FindUserByNameAsync(string name);
}