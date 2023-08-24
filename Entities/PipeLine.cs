using BashScriptRunner.HostedServices;
public class Pipeline
{
    public string? Name { get; set; }
    public List<JobService> Jobs { get; set; } = new List<JobService>();
    public PipelineState State { get; set; } = new PipelineState();

    public void Run()
    {
        Console.WriteLine($"{Name} is received.");
        foreach (var job in Jobs)
        {
            Console.WriteLine($"{job.Name} is running.");
            job.Run();
        }
    }
}
