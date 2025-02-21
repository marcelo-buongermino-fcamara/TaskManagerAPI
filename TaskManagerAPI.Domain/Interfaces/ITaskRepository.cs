using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Domain.Interfaces;

public interface ITaskRepository
{
    Task<List<ToDoItem>> GetAllAsync(Status? status, DateTime? expiresIn);
    Task<ToDoItem?> GetByIdAsync(Guid id);
    Task CreateAsync(ToDoItem task);
    Task UpdateAsync(ToDoItem task);
    Task DeleteAsync(ToDoItem task);
}