using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Endpoints;
using Taller.Hubs;
using Taller.Repository;
using Taller.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<AiService>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3001")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapHub<TaskHub>("/taskHub");
app.MapTasks();

app.Run();

