using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Common.Results;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [SwaggerOperation(Summary = "Create a task")]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "List all tasks using filters by status, date or raw")]
    public async Task<IActionResult> GetAction(Status? status, DateTime? expiresIn)
    {
        var result = await _listTasksUseCase.ExecuteAsync(status, expiresIn);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [SwaggerOperation(Summary = "Get a task by ID")]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [SwaggerOperation(Summary = "Update an existant task")]
    public async Task<IActionResult> UpdateTask(ToDoItemRequest task, Guid id)
    {
        _logger.LogInformation("Updating task with with ID {id}", id);
        var result = await _updateTaskUseCase.ExecuteAsync(task, id);

        if (result.IsFailure)
        {
            _logger.LogError("Error updating task: {Error}", result.Error);

            if (result.Error.Code == TaskManagementErrors.NotFound(id).Code)
                return NotFound(result.Error);
            else 
                return BadRequest(result.Error);
        }

        _logger.LogInformation("Task with with ID {id} has been updated", id);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [SwaggerOperation(Summary = "Delete an existant task")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var result = await _deleteTaskUseCase.ExecuteAsync(id);

        if (result.IsFailure)
        {
            _logger.LogError("Error deleting task: {Error}", result.Error);
            return NotFound(result.Error);
        }

        _logger.LogInformation("Task with with ID: {id} has been deleted", id);
        return NoContent();
    }
}