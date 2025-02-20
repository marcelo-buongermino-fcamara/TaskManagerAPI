using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Domain.Entities;

namespace TaskManagerAPI.Infrastructure;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
        PopulateDB();
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>().HasKey(x => x.ID);
        modelBuilder.Entity<ToDoItem>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<ToDoItem>().Property(x => x.Description).IsRequired(false);
        modelBuilder.Entity<ToDoItem>().Property(x => x.ExpiresIn).IsRequired(false);
        modelBuilder.Entity<ToDoItem>().Property(x => x.Status);
        modelBuilder.Entity<ToDoItem>().Property(x => x.CreatedAt).IsRequired();
        modelBuilder.Entity<ToDoItem>().Property(x => x.UpdatedAt).IsRequired();
    }

    public void PopulateDB()
    {
        if (!ToDoItems.Any())
        {
            var tasks = new List<ToDoItem>
            {
                new("Task 1", "Description 1", DateTime.UtcNow.AddDays(7), Domain.Enums.Status.Pending),
                new("Task 2", "Description 2", DateTime.UtcNow.AddDays(14), Domain.Enums.Status.InProgress),
                new("Task 3", "Description 3", DateTime.UtcNow.AddDays(21), Domain.Enums.Status.Done),
                new("Task 4", "Description 4", DateTime.UtcNow.AddDays(2), Domain.Enums.Status.Pending),
                new("Task 5", "Description 5", DateTime.UtcNow.AddDays(1), Domain.Enums.Status.InProgress),
            };

            ToDoItems.AddRange(tasks);
            SaveChanges();
        }
    }
    
}
