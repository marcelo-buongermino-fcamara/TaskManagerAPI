using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.Validators;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Interfaces;
using TaskManagerAPI.Infrastructure;

namespace TaskManagerAPI.Application.UseCases;

public interface ICreateTask
{
    Task ExecuteAsync(ToDoItemRequest toDoItem);
}

public class CreateTask(ITaskRepository repository) : ICreateTask
{
    readonly ITaskRepository _repository = repository;

    public Task ExecuteAsync(ToDoItemRequest toDoItem)
    {
        var validator = new ToDoItemRequestValidator();
        var validationResult = validator.Validate(toDoItem);

        if (!validationResult.IsValid)
        {
            //Lançar erro com Status code 404
            throw new Exception(validationResult.ToString());
        }

        var entity = new ToDoItem(
            toDoItem.Title,
            toDoItem.Description,
            toDoItem.ExpiresIn,
            toDoItem.Status
        );

        _repository.CreateAsync(entity);

        return Task.CompletedTask;
    }
}