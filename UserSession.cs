
public class UserSession
{
    public User? CurrentUser;   // null — никто не вошёл

    public bool IsLoggedIn()
    {
        return CurrentUser != null;
    }
}
