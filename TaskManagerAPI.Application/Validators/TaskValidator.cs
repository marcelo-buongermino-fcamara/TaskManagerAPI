using FluentValidation;
using TaskManagerAPI.Application.DTOs;

namespace TaskManagerAPI.Application.Validators;

public class TaskValidator : AbstractValidator<TaskRequest>
{
    public TaskValidator()
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
            .GreaterThan(DateTime.Now)
            .WithMessage("ExpiresIn must be greater than the current date");

        RuleFor(x => x.Status).IsInEnum()
            .WithMessage("Invalid status");
    }
}