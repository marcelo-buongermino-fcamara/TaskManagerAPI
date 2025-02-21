using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.API.Controllers;

[Route("api/task")]
[ApiController]
public class TasksController(ICreateTask createTask,
                            IGetById getTaskById,
                            IListTasks listTasks,
                            IUpdateTask updateTask,
                            IDeleteTask deleteTask) : ControllerBase
{
    //injetar logger e use case
    readonly ICreateTask _createTaskUseCase = createTask;
    readonly IListTasks _listTasksUseCase = listTasks;
    readonly IGetById _getTaskByIdUseCase = getTaskById;
    readonly IUpdateTask _updateTaskUseCase = updateTask;
    readonly IDeleteTask _deleteTaskUseCase = deleteTask;

    [HttpPost]
    public async Task<IActionResult> CreateTask(ToDoItemRequest task)
    {
        var result = await _createTaskUseCase.ExecuteAsync(task);

        if (result.IsFailure) 
        {
            return BadRequest(result.Error);
        }

        var created = result.Value;

        return CreatedAtAction(nameof(GetById), new { id = result.Value.ID }, result.Value);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAction(Status? status, DateTime? expiresIn)
    {
        var tasks = await _listTasksUseCase.ExecuteAsync(status, expiresIn);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getTaskByIdUseCase.ExecuteAsync(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(ToDoItemRequest task, Guid id)
    {
        await _updateTaskUseCase.ExecuteAsync(task, id);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _deleteTaskUseCase.ExecuteAsync(id);
        return NoContent();
    }
}