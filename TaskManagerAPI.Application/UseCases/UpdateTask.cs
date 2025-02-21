using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.Validators;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IUpdateTask
{
    Task ExecuteAsync(ToDoItemRequest updateRequest, Guid id);
}

public class UpdateTask(ITaskRepository repository) : IUpdateTask
{
    readonly ITaskRepository _repository = repository;

    public async Task ExecuteAsync(ToDoItemRequest updateRequest, Guid id)
    {
        var validator = new ToDoItemRequestValidator();
        var validationResult = validator.Validate(updateRequest);

        if (!validationResult.IsValid)
        {
            //Lançar erro com Status code 400
            throw new Exception(validationResult.ToString());
        }

        var result = await _repository.GetByIdAsync(id) ?? throw new Exception("Task not found"); //lançar 404

        result.Update(
              updateRequest.Title,
              updateRequest.Description,
              updateRequest.ExpiresIn,
              updateRequest.Status
        );

        await _repository.UpdateAsync(result);
    }
}
