using Microsoft.AspNetCore.SignalR;

public class PipelineStateHub : Hub
{
    public async Task SendMessage(string output)
    {
        await Clients.All.SendAsync("outputReceived", output);
    }
}