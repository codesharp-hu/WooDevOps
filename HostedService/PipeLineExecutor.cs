using System.Threading.Channels;

namespace BashScriptRunner.HostedServices;

public class PipelineExecutor : BackgroundService
{
    private readonly Channel<Pipeline> channel;

    public PipelineExecutor(Channel<Pipeline> channel)
    {
        this.channel = channel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var pipeline in channel.Reader.ReadAllAsync(stoppingToken))
        {
            Console.WriteLine($"PipelineExecutor {pipeline.Name} is received.");
            pipeline.Run();

        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        channel.Writer.Complete();
        await base.StopAsync(cancellationToken);
    }
}