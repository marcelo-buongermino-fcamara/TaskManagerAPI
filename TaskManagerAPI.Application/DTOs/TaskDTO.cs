using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Application.DTOs;

public class ToDoItemRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? ExpiresIn { get; set; }
    public Status Status { get; set; }
}

public record ToDoItemResponse(string Title, string? Description, DateTime? ExpiresIn, Status Status)
{ 
    public string Title { get;} = Title;
    public string? Description { get;} = Description;
    public DateTime? ExpiresIn { get;} = ExpiresIn;
    public Status Status { get;} = Status;

    public static ToDoItemResponse ToDTO(ToDoItemRequest entity)
    {
        return new ToDoItemResponse(entity.Title, entity.Description, entity.ExpiresIn, entity.Status);
    }
}