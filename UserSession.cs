
using ToDoApp.Users;
namespace ToDoApp;

public class UserSession
{
    public User? CurrentUser = new User
    {
        Id = 1,
        Name = ""
    };

    public bool IsLoggedIn()
    {
        return CurrentUser != null;
    }
}
