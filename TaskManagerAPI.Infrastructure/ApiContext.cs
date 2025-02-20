using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Domain.Entities;

namespace TaskManagerAPI.Infrastructure;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>().HasKey(x => x.ID);
        modelBuilder.Entity<ToDoItem>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<ToDoItem>().Property(x => x.Description).IsRequired(false);
        modelBuilder.Entity<ToDoItem>().Property(x => x.ExpiresIn).IsRequired(false);
        modelBuilder.Entity<ToDoItem>().Property(x => x.Status).IsRequired(false);
        modelBuilder.Entity<ToDoItem>().Property(x => x.CreatedAt).IsRequired();
        modelBuilder.Entity<ToDoItem>().Property(x => x.UpdatedAt).IsRequired();
    }
}
