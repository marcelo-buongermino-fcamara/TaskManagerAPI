using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Application.DTOs;

public class TaskRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? ExpiresIn { get; set; }
    public Status Status { get; set; }
}

public record TaskResponse
{ 
    public string Title { get;}
    public string? Description { get;}
    public DateTime? ExpiresIn { get;}
    public Status Status { get;}
}