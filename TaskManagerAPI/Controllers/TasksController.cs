using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.API.Controllers;

[ApiController]
[Route("api/task")]
[Produces("application/json")]
public class TasksController(ICreateTask createTask,
                            IGetById getTaskById,
                            IListTasks listTasks,
                            IUpdateTask updateTask,
                            IDeleteTask deleteTask,
                            ILogger<TasksController> logger) : ControllerBase
{
    
    private readonly ILogger<TasksController> _logger = logger;

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
            _logger.LogError("Error creating task: {Error}", result.Error);
            return BadRequest(result.Error);
        }

        var created = result.Value;

        _logger.LogInformation("Task with with ID {id} has been created", created.ID);
        return CreatedAtAction(nameof(GetById), new { id = result.Value.ID }, result.Value);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAction(Status? status, DateTime? expiresIn)
    {
        var result = await _listTasksUseCase.ExecuteAsync(status, expiresIn);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getTaskByIdUseCase.ExecuteAsync(id);

        if (result.IsFailure)
        {
            _logger.LogError("Error while finding task: {Error}", result.Error);
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(ToDoItemRequest task, Guid id)
    {
        _logger.LogInformation("Updating task with with ID {id}", id);
        await _updateTaskUseCase.ExecuteAsync(task, id);

        _logger.LogInformation("Task with with ID {id} has been updated", id);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _deleteTaskUseCase.ExecuteAsync(id);

        _logger.LogInformation("Task with with ID: {id} has been deleted", id);
        return NoContent();
    }
}