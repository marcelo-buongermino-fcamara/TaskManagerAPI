using FluentValidation;
using TaskManagerAPI.Application.DTOs;

namespace TaskManagerAPI.Application.Validators;

public class ToDoItemRequestValidator : AbstractValidator<ToDoItemRequest>
{
    public ToDoItemRequestValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(50)
            .WithMessage("Title must have a maximum of 50 characters");

        RuleFor(p => p.Description)
            .MaximumLength(100)
            .WithMessage("Description must have a maximum of 200 characters");
    
        RuleFor(p => p.ExpiresIn)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("ExpiresIn must be greater or equal than the current date");

        RuleFor(x => x.Status).IsInEnum()
            .WithMessage("Invalid status");
    }
}