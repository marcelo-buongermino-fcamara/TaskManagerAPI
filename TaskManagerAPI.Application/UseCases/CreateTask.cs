using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.Validators;
using TaskManagerAPI.Domain.Common.Results;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface ICreateTask
{
    Task<Result<ToDoItemResponse>> ExecuteAsync(ToDoItemRequest toDoItem);
}

public class CreateTask(ITaskRepository repository) : ICreateTask
{
    private readonly ITaskRepository _repository = repository;

    public async Task<Result<ToDoItemResponse>> ExecuteAsync(ToDoItemRequest toDoItem)
    {
        var validator = new ToDoItemRequestValidator();
        var validationResult = validator.Validate(toDoItem);

        if (!validationResult.IsValid)
        {
            return Result<ToDoItemResponse>.Failure(
                TaskManagementErrors.ValidationError(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var entity = new ToDoItem(
            toDoItem.Title,
            toDoItem.Description,
            toDoItem.ExpiresIn,
            toDoItem.Status
        );

        await _repository.CreateAsync(entity);

        var response = ToDoItemResponse.ToDTO(entity);

        return Result<ToDoItemResponse>.Success(response);
    }
}