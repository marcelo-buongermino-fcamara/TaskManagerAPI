using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IDeleteTask
{
    Task ExecuteAsync(Guid id);
}

public class DeleteTask(ITaskRepository repository) : IDeleteTask
{
    readonly ITaskRepository _repository = repository;

    public async Task ExecuteAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id) ?? throw new Exception("Not Found");

        await _repository.DeleteAsync(item);
    }
}