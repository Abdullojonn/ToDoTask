using ToDoApp;

public interface IUserStorage
{
    List<User> LoadUsers();
    void SaveUser(User user);
}