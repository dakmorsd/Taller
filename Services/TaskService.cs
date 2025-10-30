using Microsoft.AspNetCore.SignalR;
using Taller.Data;
using Taller.Hubs;
using Taller.Repository;

namespace Taller.Services;

public class TaskService(TaskRepository repository, IHubContext<TaskHub> hubContext, ILogger<TaskService> logger)
{
    public async Task<List<TaskItem>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        try
        {
            return await repository.GetAllAsync(page, pageSize);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error");

            return [];
        }
    }

    public async Task<TaskItem?> AddAsync(string title, string description)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                logger.LogError("Missing Title");
                
                return null;
            }

            var task = await repository.AddAsync(title, description);
            
            await hubContext.Clients.All.SendAsync("TaskAdded", task);

            logger.LogInformation("Added: {TaskId}", task.Id);
            
            return task;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error");

            return null;
        }
    }
}
