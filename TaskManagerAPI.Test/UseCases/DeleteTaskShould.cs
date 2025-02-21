using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Tests.UseCases;

public class DeleteTaskShould
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly Mock<ILogger<DeleteTask>> _loggerMock;
    private readonly DeleteTask _deleteTask;

    public DeleteTaskShould()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _loggerMock = new Mock<ILogger<DeleteTask>>();
        _deleteTask = new DeleteTask(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ReturnFailureResultWhenTaskNotFound()
    {
        // Arrange
        var taskId = Guid.NewGuid();

        _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ToDoItem?)null);

        // Act
        var result = await _deleteTask.ExecuteAsync(taskId);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"The task with Id '{taskId}' was not found", result.Error.Description);
    }

    [Fact]
    public async Task ReturnSuccessResultWhenTaskIsDeleted()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var existingTask = new ToDoItem("Title", "Description", DateTime.Now.AddDays(1), Status.Pending);

        _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingTask);

        _repositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<ToDoItem>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _deleteTask.ExecuteAsync(taskId);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
