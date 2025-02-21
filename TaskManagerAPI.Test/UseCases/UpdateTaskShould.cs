using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Tests.UseCases;

public class UpdateTaskShould
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly Mock<ILogger<UpdateTask>> _loggerMock;
    private readonly UpdateTask _updateTask;

    public UpdateTaskShould()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _loggerMock = new Mock<ILogger<UpdateTask>>();
        _updateTask = new UpdateTask(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ReturnFailureResultWhenValidationFails()
    {
        // Arrange
        var invalidRequest = new ToDoItemRequest
        {
            Title = "", // Invalid title
            Description = "Description",
            ExpiresIn = DateTime.Now.AddDays(1),
            Status = Status.Pending
        };

        // Act
        var result = await _updateTask.ExecuteAsync(invalidRequest, Guid.NewGuid());

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Title", result.Error.Description);
    }

    [Fact]
    public async Task ReturnFailureResultWhenTaskNotFound()
    {
        // Arrange
        var taskID = Guid.NewGuid();

        var validRequest = new ToDoItemRequest
        {
            Title = "Valid Title",
            Description = "Description",
            ExpiresIn = DateTime.Now.AddDays(1),
            Status = Status.Pending
        };

        _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ToDoItem?)null);

        // Act
        var result = await _updateTask.ExecuteAsync(validRequest, taskID);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"The task with Id '{taskID}' was not found", result.Error.Description);
    }

    [Fact]
    public async Task ReturnSuccessResultWhenTaskIsUpdated()
    {
        // Arrange
        var validRequest = new ToDoItemRequest
        {
            Title = "Valid Title",
            Description = "Description",
            ExpiresIn = DateTime.Now.AddDays(1),
            Status = Status.Pending
        };

        var existingTask = new ToDoItem("Old Title", "Old Description", DateTime.Now.AddDays(2), Status.InProgress);

        _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingTask);

        _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<ToDoItem>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _updateTask.ExecuteAsync(validRequest, Guid.NewGuid());

        // Assert
        Assert.True(result.IsSuccess);
    }
}
