using Microsoft.AspNetCore.SignalR;

namespace AgendaUpc.Hubs;

public class OrdensHub : Hub
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine("--> Conexión establecida " + Context.ConnectionId);

        return base.OnConnectedAsync();
    }
}