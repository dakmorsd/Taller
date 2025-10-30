using Microsoft.EntityFrameworkCore;
using Taller.Data;

namespace Taller.Repository;

public class TaskRepository(TaskDbContext context)
{
    public async Task<List<TaskItem>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await context.Tasks
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<TaskItem> AddAsync(string title, string description)
    {
        var task = new TaskItem
        {
            Title = title,
            Description = description
        };

        context.Tasks.Add(task);

        await context.SaveChangesAsync();
        
        return task;
    }
}
