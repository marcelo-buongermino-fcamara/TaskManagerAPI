using Microsoft.Extensions.Logging;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.Validators;
using TaskManagerAPI.Domain.Common.Results;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IUpdateTask
{
    Task<Result> ExecuteAsync(ToDoItemRequest updateRequest, Guid id);
}

public class UpdateTask(ITaskRepository repository, ILogger<UpdateTask> logger) : IUpdateTask
{
    private readonly ITaskRepository _repository = repository;
    private readonly ILogger<UpdateTask> _logger = logger;

    public async Task<Result> ExecuteAsync(ToDoItemRequest updateRequest, Guid id)
    {
        _logger.LogInformation("UpdateTask UseCase. Task with ID: {id}", id);

        var validator = new ToDoItemRequestValidator();
        var validationResult = validator.Validate(updateRequest);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Validation failed for Task with ID: {id}", id);

            return Result.Failure(
                TaskManagementErrors.ValidationError(validationResult.Errors.Select(e => e.ErrorMessage)));            
        }

        var result = await _repository.GetByIdAsync(id);

        if (result is null) 
        {
            _logger.LogError("Task with ID: {id} not found", id);
            return Result.Failure(TaskManagementErrors.NotFound(id));
        }

        result.Update(
            updateRequest.Title,
            updateRequest.Description,
            updateRequest.ExpiresIn,
            updateRequest.Status
        );

        await _repository.UpdateAsync(result);

        return Result.Success();
    }
}
