using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Domain.Common.Results;
using TaskManagerAPI.Domain.Enums;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IListTasks
{
    Task<Result<List<ToDoItemResponse>>> ExecuteAsync(Status? status, DateTime? expiresIn);
}

public class ListTasks(ITaskRepository repository) : IListTasks
{
    private readonly ITaskRepository _repository = repository;

    async Task<Result<List<ToDoItemResponse>>> IListTasks.ExecuteAsync(Status? status, DateTime? expiresIn)
    {
        var toDoItems = await _repository.GetAllAsync(status, expiresIn);
        var itemsDTO = new List<ToDoItemResponse>();

        foreach (var toDoItem in toDoItems)
        {
            itemsDTO.Add(ToDoItemResponse.ToDTO(toDoItem));
        }

        return Result<List<ToDoItemResponse>>.Success(itemsDTO);
    } 
}