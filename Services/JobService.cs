using System.Diagnostics;
using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;

namespace BashScriptRunner.HostedServices;
public class JobService : BackgroundService
{
    private Channel<JobTask> scriptTaskChannel { get; }
    public PipelineState pipelineState { get; set; }
    private IHubContext<PipelineStateHub> hubContext { get; }

    public JobService(Channel<JobTask> scriptTaskChannel, PipelineState pipelineState, IHubContext<PipelineStateHub> hubContext)
    {
        this.scriptTaskChannel = scriptTaskChannel;
        this.pipelineState = pipelineState;
        this.hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var scriptTask in scriptTaskChannel.Reader.ReadAllAsync(stoppingToken))
        {
            var jobState = new JobState
            {
                State = State.InProgress
            };
            pipelineState.JobStates.Add(jobState);
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
                    await hubContext.Clients.All.SendAsync("outputReceived", e.Data);
                    pipelineState.JobStates[pipelineState.JobStates.Count - 1].Messages.Add(e.Data);
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
