using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Tests.UseCases;

public class GetByIdShould
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly Mock<ILogger<GetById>> _loggerMock;
    private readonly IGetById _getById;

    public GetByIdShould()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _loggerMock = new Mock<ILogger<GetById>>();
        _getById = new GetById(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ReturnFailureResultWhenTaskNotFound()
    {
        // Arrange
        var taskId = Guid.NewGuid();

        _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ToDoItem?)null);

        // Act
        var result = await _getById.ExecuteAsync(taskId);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"The task with Id '{taskId}' was not found", result.Error.Description);
    }

    [Fact]
    public async Task ReturnSuccessResultWhenTaskIsFound()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var existingTask = new ToDoItem("Title", "Description", DateTime.Now.AddDays(1), Status.Pending);

        _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingTask);

        // Act
        var result = await _getById.ExecuteAsync(taskId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(existingTask.Title, result.Value.Title);
    }
}