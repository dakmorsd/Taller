using Microsoft.AspNetCore.SignalR;

namespace Taller.Hubs;

public class TaskHub : Hub
{
    public async Task NotifyTaskAdded(object task)
    {
        await Clients.All.SendAsync("TaskAdded", task);
    }
}
