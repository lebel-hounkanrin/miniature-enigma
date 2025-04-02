using Microsoft.AspNetCore.SignalR;

namespace parc.Hubs;

public class CommandHub: Hub
{
    public async Task ExecuteCommand(string command)
    {
        await Clients.All.SendAsync("ReceiveCommand", command);
    }
}