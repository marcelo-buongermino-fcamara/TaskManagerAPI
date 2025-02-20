using Microsoft.EntityFrameworkCore;
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

    public async Task<List<ToDoItem>> GetAllAsync(Status? status, DateTime? expiresIn)
    {
        var query = _context.ToDoItems.AsQueryable();

        if (status.HasValue)
            query = query.Where(p => p.Status == status.Value);

        if (expiresIn.HasValue)
            query = query.Where(p => p.ExpiresIn == expiresIn.Value);

        return await query.ToListAsync();
    }

    public Task<ToDoItem> UpdateAsync(ToDoItem task)
    {
        throw new NotImplementedException();
    }
}   
    