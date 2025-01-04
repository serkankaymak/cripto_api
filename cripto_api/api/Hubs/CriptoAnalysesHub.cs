using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace api.Hubs;

[AllowAnonymous]
public class CriptoAnalysesHub : Hub<ICriptoAnalysesHub>
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine(" OnConnectedAsync executed");
        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine(" OnDisconnectedAsync executed");
        return base.OnDisconnectedAsync(exception);
    }
}

