using TaskManagerAPI.Application.DTOs;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Tests.DTOs;

public class ToDoItemResponseToDTO
{
    [Fact]
    public void ToDTO_ShouldConvertToDoItemToToDoItemResponse()
    {
        // Arrange
        var toDoItem = new ToDoItem("Test Title", "Test Description", DateTime.Now.AddDays(1), Status.Pending);

        // Act
        var toDoItemResponse = ToDoItemResponse.ToDTO(toDoItem);

        // Assert
        Assert.Equal(toDoItem.ID, toDoItemResponse.ID);
        Assert.Equal(toDoItem.Title, toDoItemResponse.Title);
        Assert.Equal(toDoItem.Description, toDoItemResponse.Description);
        Assert.Equal(toDoItem.ExpiresIn, toDoItemResponse.ExpiresIn);
        Assert.Equal(toDoItem.Status.ToString(), toDoItemResponse.Status);
    }
}

