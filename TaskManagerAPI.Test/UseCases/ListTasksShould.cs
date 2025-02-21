using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Tests.UseCases;

public class ListTasksShould
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly Mock<ILogger<ListTasks>> _loggerMock;
    private readonly IListTasks _listTasks;

    public ListTasksShould()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _loggerMock = new Mock<ILogger<ListTasks>>();
        _listTasks = new ListTasks(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ReturnAllTasksWhenNoFiltersApplied()
    {
        // Arrange
        var tasks = new List<ToDoItem>
        {
            new ToDoItem("Task 1", "Description 1", DateTime.Now.AddDays(1), Status.Pending),
            new ToDoItem("Task 2", "Description 2", DateTime.Now.AddDays(2), Status.InProgress)
        };

        _repositoryMock.Setup(repo => repo.GetAllAsync(null, null))
            .ReturnsAsync(tasks);

        // Act
        var result = await _listTasks.ExecuteAsync(null, null);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
    }

    [Fact]
    public async Task ReturnFilteredTasksByStatus()
    {
        // Arrange
        var tasks = new List<ToDoItem>
        {
            new ToDoItem("Task 1", "Description 1", DateTime.Now.AddDays(1), Status.Pending),
            new ToDoItem("Task 2", "Description 2", DateTime.Now.AddDays(2), Status.Pending)
        };

        _repositoryMock.Setup(repo => repo.GetAllAsync(Status.Pending, null))
            .ReturnsAsync(tasks);

        // Act
        var result = await _listTasks.ExecuteAsync(Status.Pending, null);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.All(result.Value, item => Assert.Equal("Pending", item.Status));
    }

    [Fact]
    public async Task ReturnFilteredTasksByExpiresIn()
    {
        // Arrange
        var expiresIn = DateTime.Now.AddDays(1);
        var tasks = new List<ToDoItem>
        {
            new ToDoItem("Task 1", "Description 1", expiresIn, Status.Pending)
        };

        _repositoryMock.Setup(repo => repo.GetAllAsync(null, expiresIn))
            .ReturnsAsync(tasks);

        // Act
        var result = await _listTasks.ExecuteAsync(null, expiresIn);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.All(result.Value, item => Assert.Equal(expiresIn, item.ExpiresIn));
    }

    [Fact]
    public async Task ReturnFilteredTasksByStatusAndExpiresIn()
    {
        // Arrange
        var expiresIn = DateTime.Now.AddDays(1);
        var tasks = new List<ToDoItem>
        {
            new ToDoItem("Task 1", "Description 1", expiresIn, Status.Pending)
        };

        _repositoryMock.Setup(repo => repo.GetAllAsync(Status.Pending, expiresIn))
            .ReturnsAsync(tasks);

        // Act
        var result = await _listTasks.ExecuteAsync(Status.Pending, expiresIn);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.All(result.Value, item => 
        {
            Assert.Equal("Pending", item.Status);
            Assert.Equal(expiresIn, item.ExpiresIn);
        });
    }
}
