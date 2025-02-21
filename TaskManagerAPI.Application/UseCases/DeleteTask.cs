using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;
using TaskManagerAPI.Domain.Common.Results;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IDeleteTask
{
    Task<Result> ExecuteAsync(Guid id);
}

public class DeleteTask(ITaskRepository repository, ILogger<DeleteTask> logger) : IDeleteTask
{
    private readonly ITaskRepository _repository = repository;
    private readonly ILogger<DeleteTask> _logger = logger;

    public async Task<Result> ExecuteAsync(Guid id)
    {
        _logger.LogInformation("DeleteTask UseCase. Task with ID: {id}", id);
        var item = await _repository.GetByIdAsync(id);

        if (item is null) 
        {
            _logger.LogError("Task with ID: {id} not found", id);
            return Result.Failure(TaskManagementErrors.NotFound(id));
        }

        await _repository.DeleteAsync(item);

        _logger.LogInformation("Task with ID: {id} has been deleted succesfully", id);
        return Result.Success();
    }
}