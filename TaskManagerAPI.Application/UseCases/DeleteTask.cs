using System.Reflection.Metadata.Ecma335;
using TaskManagerAPI.Domain.Common.Results;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IDeleteTask
{
    Task<Result> ExecuteAsync(Guid id);
}

public class DeleteTask(ITaskRepository repository) : IDeleteTask
{
    private readonly ITaskRepository _repository = repository;

    public async Task<Result> ExecuteAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id); 
        
        if (item is null)
            return Result.Failure(TaskManagementErrors.NotFound(id));

        await _repository.DeleteAsync(item);

        return Result.Success();
    }
}