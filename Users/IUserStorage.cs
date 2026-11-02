using ToDoApp;
using ToDoApp.Users;

public interface IUserStorage
{
    List<User> LoadUsers();
    void SaveUser(User user);
    User? FindUserByName(string name);
}