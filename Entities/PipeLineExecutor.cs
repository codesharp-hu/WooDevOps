using System.Threading.Channels;

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
            pipeline.Run();
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        channel.Writer.Complete();
        await base.StopAsync(cancellationToken);
    }
}