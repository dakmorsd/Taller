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

        application.MapGet("/tasks/summary", async (TaskService taskService, AiService aiService) =>
        {
            var tasks = await taskService.GetAllAsync(1, 1000);
            var descriptions = tasks.Select(t => t.Description).ToList();
            var summary = await aiService.GetTasksSummaryAsync(descriptions);

            return Results.Ok(new { summary });
        })
        .WithName("GetTasksSummary");
    }
}