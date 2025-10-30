using Taller.Services;

namespace Taller.Endpoints;

public static class TasksEndpoint
{
    public static void MapTasks(this WebApplication application)
    {
        application.MapGet("/tasks", async (TaskService service, int page = 1, int pageSize = 10) =>
        {
            var tasks = await service.GetAllAsync(page, pageSize);

            return Results.Ok(tasks);
        })
        .WithName("GetTasks");

        application.MapPost("/tasks", async (TaskService service, string title, string description) =>
        {
            var task = await service.AddAsync(title, description);

            return task != null ?
                Results.Created($"/tasks/{task.Id}", task) : 
                Results.BadRequest();
        })
        .WithName("AddTask");
    }
}