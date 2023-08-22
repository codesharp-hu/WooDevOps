using System.Threading.Channels;

public class PipelineExecutor : BackgroundService
{
    private readonly Channel<Pipeline> _channel = Channel.CreateUnbounded<Pipeline>();

    public void EnqueuePipeline(Pipeline pipeline)
    {
        _channel.Writer.TryWrite(pipeline);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var pipeline in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            Console.WriteLine($"{pipeline.Name} is received.");
            foreach (var job in pipeline.Jobs)
            {
                Console.WriteLine($"{job.Name} is running.");
                job.Run();
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Writer.Complete();
        await base.StopAsync(cancellationToken);
    }
}