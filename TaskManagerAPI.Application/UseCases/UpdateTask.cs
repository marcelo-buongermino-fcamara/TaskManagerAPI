using TaskManagerAPI.Application.DTOs;

namespace TaskManagerAPI.Application.UseCases;

public interface IUpdateTask
{
    Task<ToDoItemResponse> ExecuteAsync(ToDoItemRequest task);
}

public class UpdateTask : IUpdateTask
{
    public Task<ToDoItemResponse> ExecuteAsync(ToDoItemRequest task)
    {
        throw new NotImplementedException();
    }
}