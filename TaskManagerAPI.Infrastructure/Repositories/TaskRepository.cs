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

    public async Task DeleteAsync(ToDoItem item)
    {
        _context.Remove(item);
        await _context.SaveChangesAsync();
        
    }

    public async Task<List<ToDoItem>> GetAllAsync(Status? status, DateTime? expiresIn)
    {
        var query = _context.ToDoItems.AsQueryable();

        if (status.HasValue)
            query = query.Where(p => p.Status == status.Value);

        if (expiresIn.HasValue)
            query = query.Where(p => p.ExpiresIn!.Value.Date == expiresIn.Value.Date);

        return await query.ToListAsync();
    }

    public async Task<ToDoItem?> GetByIdAsync(Guid id)
    {
        return await _context.ToDoItems.FindAsync(id);
    }

    public async Task UpdateAsync(ToDoItem task)
    {
        _context.Update(task);
        await _context.SaveChangesAsync();
    }
}   
    