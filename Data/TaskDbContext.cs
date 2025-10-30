using Microsoft.EntityFrameworkCore;

namespace Taller.Data;

public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks { get; set; }
}
