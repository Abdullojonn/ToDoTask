using Microsoft.EntityFrameworkCore;
using ToDoApp.Tasks;
using ToDoApp.Users;

namespace ToDoApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<TodoTask> Tasks => Set<TodoTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        
        modelBuilder.Entity<User>().Property(user => user.Name).IsRequired();
        modelBuilder.Entity<User>().Property(user => user.Password).IsRequired();
        modelBuilder.Entity<User>().HasIndex(user => user.Name).IsUnique();

        modelBuilder.Entity<TodoTask>().ToTable("tasks");
        modelBuilder.Entity<TodoTask>().Property(task => task.Title).IsRequired();
        
    }
}
