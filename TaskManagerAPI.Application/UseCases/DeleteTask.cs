using System.Runtime.CompilerServices;
using TaskManagerAPI.Application.DTOs;

namespace TaskManagerAPI.Application.UseCases;

public interface IDeleteTask
{
    void ExecuteAsync(Guid id);
}

public class DeleteTask : IDeleteTask
{
    public void ExecuteAsync(Guid id)
    {

    }
}