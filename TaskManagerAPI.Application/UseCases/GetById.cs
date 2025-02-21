using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Domain.Common.Results;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IGetById
{
    Task<Result<ToDoItemResponse>> ExecuteAsync(Guid id);
}

public class GetById(ITaskRepository repository) : IGetById
{
    private readonly ITaskRepository _repository = repository;

    async Task<Result<ToDoItemResponse>> IGetById.ExecuteAsync(Guid id)
    {
        var toDoItem = await _repository.GetByIdAsync(id);

        if (toDoItem is null) {
            return Result<ToDoItemResponse>.Failure(TaskManagementErrors.NotFound(id));
        }

        var toDoItemDTO = ToDoItemResponse.ToDTO(toDoItem);

        return Result<ToDoItemResponse>.Success(toDoItemDTO);
    }
}
