using ToDoApp;
using ToDoApp.Users;

public interface IUserStorage
{
    Task<List<User>> LoadUsersAsync();
    Task SaveUserAsync(User user);
    Task<User?> FindUserByNameAsync(string name);
}