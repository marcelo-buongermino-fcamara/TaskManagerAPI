using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Infrastructure.Repositories;

public class TaskRepository(ApiContext context) : ITaskRepository
{
    private readonly ApiContext _context = context;

    public async Task CreateAsync(ToDoItem task)
    {
        _context.ToDoItems.Add(task);
        await _context.SaveChangesAsync();
    }

    public Task<ToDoItem> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ToDoItem>> GetAllAsync(Status? status, DateTime? expiresIn)
    {
        throw new NotImplementedException();
    }

    public Task<ToDoItem> UpdateAsync(ToDoItem task)
    {
        throw new NotImplementedException();
    }
}   
    