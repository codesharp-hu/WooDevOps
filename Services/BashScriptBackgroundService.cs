using System.Diagnostics;
using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;

namespace BashScriptRunner.HostedServices;
public class BashScriptBackgroundService : BackgroundService
{
    private Channel<ScriptTask> scriptTaskChannel { get; }
    public ScriptState scriptState { get; set; }
    private IHubContext<ScriptStateHub> hubContext { get; }

    public BashScriptBackgroundService(Channel<ScriptTask> scriptTaskChannel, ScriptState scriptState, IHubContext<ScriptStateHub> hubContext)
    {
        this.scriptTaskChannel = scriptTaskChannel;
        this.scriptState = scriptState;
        this.hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var scriptTask in scriptTaskChannel.Reader.ReadAllAsync(stoppingToken))
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
                    await hubContext.Clients.All.SendAsync("outputReceived", output);
                    scriptState.Outputs.Add(output);
                    Console.WriteLine($"Output: {e.Data}");
                    Console.WriteLine($"Script state: {scriptState.Outputs.Count}");
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
