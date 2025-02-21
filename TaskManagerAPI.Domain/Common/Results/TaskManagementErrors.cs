namespace TaskManagerAPI.Domain.Common.Results;

public static class TaskManagementErrors
{
    public static Error ValidationError(IEnumerable<string> errors)
    {
        string message = string.Join("; ", errors);
        return new Error("Task.ValidationError", message);
    }

    public static Error NotFound(Guid id) => new Error(
        "Task.NotFound", $"The taks with Id '{id}' was not found");
}
