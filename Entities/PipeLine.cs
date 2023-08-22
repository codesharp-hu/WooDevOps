using BashScriptRunner.HostedServices;
public class Pipeline
{
    public string? Name { get; set; }
    public List<JobService> Jobs { get; set; } = new List<JobService>();
    public PipelineState State { get; set; } = new PipelineState();

    private static readonly PipelineExecutor _executor = new PipelineExecutor();

    public void Run()
    {
        Console.WriteLine($"{Name} is running.");
        _executor.StartAsync(CancellationToken.None);
        _executor.EnqueuePipeline(this);
    }
}
