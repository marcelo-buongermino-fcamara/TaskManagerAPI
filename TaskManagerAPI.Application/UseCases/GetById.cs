using Microsoft.Extensions.Logging;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Domain.Common.Results;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IGetById
{
    Task<Result<ToDoItemResponse>> ExecuteAsync(Guid id);
}

public class GetById(ITaskRepository repository, ILogger<GetById> logger) : IGetById
{
    private readonly ITaskRepository _repository = repository;
    private readonly ILogger<GetById> _logger = logger;

    async Task<Result<ToDoItemResponse>> IGetById.ExecuteAsync(Guid id)
    {
        _logger.LogInformation("GetById UseCase. Task with ID: {id}", id);

        var toDoItem = await _repository.GetByIdAsync(id);

        if (toDoItem is null) {
            _logger.LogError("Task with ID: {id} not found", id);

            return Result<ToDoItemResponse>.Failure(TaskManagementErrors.NotFound(id));
        }

        var toDoItemDTO = ToDoItemResponse.ToDTO(toDoItem);

        return Result<ToDoItemResponse>.Success(toDoItemDTO);
    }
}
