public class UserStorage : IUserStorage
{
    public const string FilePath = "users.csv";
    public List<User> LoadUsers()
    {
        List<User> user = new List<User>();

        if (!File.Exists(FilePath))
        {
            return user;
        }

        string[] parts = File.ReadAllLines(FilePath);

        foreach(var i in parts)
        {
            user.Add(FromCsvLine(i));
        }

        return user;
    }
    public void SaveUser(User user)
    {
        List<User> users = LoadUsers();

        int mx = 0;

        foreach(var u in users)
        {
            if(u.Id > mx)
            {
                mx = u.Id;
            }
        }
            user.Id = mx + 1;

            string line = ToCsvLine(user);
            File.AppendAllText(FilePath, line + "\n");
    }
    private static string ToCsvLine(User user)
    {
        return $"{user.Id},{user.Name}";
    }
    private static User FromCsvLine(string line)
    {
        User user = new User();
        string[] parts = line.Split(",");

        user.Id = int.Parse(parts[0]);
        user.Name = parts[1];

        return user;
    }
}