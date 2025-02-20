using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Application.UseCases;

public interface IListTasks
{
    IEnumerable<Task<ToDoItemResponse>> ExecuteAsync(Status status, DateTime expiresIn);
}

public class ListTasks : IListTasks
{
    IEnumerable<Task<ToDoItemResponse>> IListTasks.ExecuteAsync(Status status, DateTime expiresIn)
    {
        throw new NotImplementedException();
    }
}