using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Domain.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<Task>> GetAllAsync(Status? status, DateTime? expiresIn);
    Task<Task> CreateAsync(Task task);
    Task<Task> UpdateAsync(Task task);
    Task<Task> DeleteAsync(Guid id);
}