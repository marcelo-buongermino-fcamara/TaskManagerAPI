using Moq;
using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;
using TaskManagerAPI.Domain.Interfaces;

namespace TaskManagerAPI.Tests.UseCases;

public class CreateTaskShould
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly CreateTask _createTask;

    public CreateTaskShould()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _createTask = new CreateTask(_repositoryMock.Object);
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
        var result = await _createTask.ExecuteAsync(invalidRequest);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Title", result.Error.Description);
    }

    [Fact]
    public async Task ReturnSuccessResultWhenTaskIsCreated()
    {
        // Arrange
        var validRequest = new ToDoItemRequest
        {
            Title = "Valid Title",
            Description = "Description",
            ExpiresIn = DateTime.Now.AddDays(1),
            Status = Status.Pending
        };

        _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<ToDoItem>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _createTask.ExecuteAsync(validRequest);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(validRequest.Title, result.Value.Title);
    }
}
