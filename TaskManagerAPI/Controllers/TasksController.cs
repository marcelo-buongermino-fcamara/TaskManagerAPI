using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.API.Controllers;

[Route("api/task")]
[ApiController]
public class TasksController : ControllerBase
{
    //injetar logger e use case
    readonly ICreateTask _createTaskUseCase;
    readonly IListTasks _listTasksUseCase;
    public TasksController(ICreateTask createTask, IListTasks listTasks)
    {
        _createTaskUseCase = createTask;
        _listTasksUseCase = listTasks;
    }


    [HttpPost]
    public async Task<IActionResult> CreateTask(ToDoItemRequest task)
    {
        await _createTaskUseCase.ExecuteAsync(task);

        return Created("", task);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAction(Status? status, DateTime? expiresIn)
    {
        var tasks = await _listTasksUseCase.ExecuteAsync(status, expiresIn);
        return Ok(tasks);
    }
}