using System.Diagnostics;
using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;

namespace BashScriptRunner.HostedServices
{
    public class JobService : BackgroundService
    {
        private Channel<JobDescriptor> channel { get; }
        public PipelineState pipelineState { get; set; }
        private IHubContext<PipelineStateHub> hubContext { get; }

        public JobService(Channel<JobDescriptor> channel, PipelineState pipelineState, IHubContext<PipelineStateHub> hubContext)
        {
            this.channel = channel;
            this.pipelineState = pipelineState;
            this.hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var jobDescriptor in channel.Reader.ReadAllAsync(stoppingToken))
            {
                Console.WriteLine($"Job received: {jobDescriptor.Name}");
                var jobState = new JobState
                {
                    State = State.InProgress
                };
                pipelineState.JobStates.Add(jobState);

                var startInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                startInfo.ArgumentList.Add("-c");
                startInfo.ArgumentList.Add(jobDescriptor.Command);

                using (var process = new Process())
                {
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

                    process.WaitForExit();
                }
            }
        }
    }
}
