using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Domain.Enums;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Application.UseCases;

public interface IListTasks
{
    Task<List<ToDoItemResponse>> ExecuteAsync(Status? status, DateTime? expiresIn);
}

public class ListTasks(ITaskRepository repository) : IListTasks
{
    readonly ITaskRepository _repository = repository;

    async Task<List<ToDoItemResponse>> IListTasks.ExecuteAsync(Status? status, DateTime? expiresIn)
    {
        var toDoItems = await _repository.GetAllAsync(status, expiresIn);
        var itemsDTO = new List<ToDoItemResponse>();

        foreach (var toDoItem in toDoItems)
        {
            itemsDTO.Add(ToDoItemResponse.ToDTO(toDoItem));
        }

        return itemsDTO;
    }  
}