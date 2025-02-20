using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Application.DTOs;

public class ToDoItemRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? ExpiresIn { get; set; }
    public Status Status { get; set; }
}

public record ToDoItemResponse(Guid ID, string Title, string? Description, DateTime? ExpiresIn, string Status)
{ 
    public Guid ID { get; } = ID;
    public string Title { get;} = Title;
    public string? Description { get;} = Description;
    public DateTime? ExpiresIn { get;} = ExpiresIn;
    public string Status { get;} = Status;

    public static ToDoItemResponse ToDTO(ToDoItem entity)
    {
        return new ToDoItemResponse(entity.ID, entity.Title, entity.Description, entity.ExpiresIn, entity.Status.ToString());
    }
}