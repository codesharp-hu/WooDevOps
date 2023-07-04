using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;

namespace BashScriptRunner;
public class BashScriptBackgroundService : BackgroundService
{

    private Channel<ScriptTask> Channel { get; }
    private Channel<ScriptState> ScriptStateChannel { get; set; }
    private IHubContext<ScriptStateHub> HubContext { get; }

    public BashScriptBackgroundService(Channel<ScriptTask> channel, Channel<ScriptState> scriptStateChannel, IHubContext<ScriptStateHub> hubContext)
    {
        Channel = channel;
        ScriptStateChannel = scriptStateChannel;
        HubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scriptState = new ScriptState { Running = true, Outputs = new List<Output>() };
        await foreach (var scriptTask in Channel.Reader.ReadAllAsync(stoppingToken))
        {
            using var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"getInfo.sh",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            process.OutputDataReceived += async (sender, e) =>
            {
                if (e.Data != null)
                {
                    var output = new Output(e.Data);
                    await HubContext.Clients.All.SendAsync("outputReceived", output);
                    scriptState.Outputs.Add(output);
                    ScriptStateChannel.Writer.TryWrite(scriptState);
                    Console.WriteLine($"Output: {e.Data}");
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    Console.WriteLine($"Error: {e.Data}");
                }
            };

            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync(stoppingToken);
        }
    }
}
