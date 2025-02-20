using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.UseCases;

namespace TaskManagerAPI.API.Controllers;

[Route("api/task")]
[ApiController]
public class TasksController : ControllerBase
{
    //injetar logger e use case
    readonly ICreateTask _createTaskUseCase;
    public TasksController(ICreateTask createTask)
    {
        _createTaskUseCase = createTask;
    }


    [HttpPost]
    public async Task<IActionResult> CreateTask(ToDoItemRequest task)
    {
        await _createTaskUseCase.ExecuteAsync(task);

        return Created("", task);
    }
}