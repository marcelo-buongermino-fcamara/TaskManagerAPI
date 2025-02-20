using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Domain.Interfaces;

public interface ITaskRepository
{
    Task<List<ToDoItem>> GetAllAsync(Status? status, DateTime? expiresIn);
    Task CreateAsync(ToDoItem task);
    Task<ToDoItem> UpdateAsync(ToDoItem task);
    Task<ToDoItem> DeleteAsync(Guid id);
}