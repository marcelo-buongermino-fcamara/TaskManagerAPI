﻿using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Domain.Entities;

public class ToDoItem(string title, string? description, DateTime? expiresIn, Status status)
{
    public Guid ID { get; } = Guid.NewGuid();
    public string Title { get; private set; } = title;
    public string? Description { get; private set; } = description;
    public DateTime? ExpiresIn { get; private set; } = expiresIn;
    public Status Status { get; private set; } = status;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    public void Update(string title, string? description, DateTime? expiresIn, Status status)
    {
        Title = title;
        Description = description;
        ExpiresIn = expiresIn;
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}