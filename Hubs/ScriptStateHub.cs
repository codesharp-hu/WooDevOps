using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ScriptStateHub : Hub
{
    public async Task SendMessage(string output)
    {
        await Clients.All.SendAsync("outputReceived", output);
    }
}