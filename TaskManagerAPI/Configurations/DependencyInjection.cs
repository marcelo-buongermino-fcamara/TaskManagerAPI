using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Application.UseCases;
using TaskManagerAPI.Domain.Interfaces;
using TaskManagerAPI.Infrastructure;
using TaskManagerAPI.Infrastructure.Repositories;

namespace TaskManagerAPI.API.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        //Application
        services.AddScoped<ICreateTask, CreateTask>();
        services.AddScoped<IUpdateTask, UpdateTask>();
        services.AddScoped<IDeleteTask, DeleteTask>();
        services.AddScoped<IListTasks, ListTasks>();
        services.AddScoped<IGetById, GetById>();

        //Domain
        services.AddScoped<ITaskRepository, TaskRepository>();

        //Infrastructure
        services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

        return services;
    }
}