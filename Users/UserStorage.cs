using ToDoApp.Data;

using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Users;

public class UserStorage(AppDbContext context) : IUserStorage
{

    public async Task SaveUserAsync(User user)
    {
        context.Users.Add(user);

        await context.SaveChangesAsync();
    }

    public async Task<User?> FindUserByNameAsync(string name)
    {
        var user = await context.Users.Where(u => u.Name == name).SingleOrDefaultAsync();

        return user;
    }
}
