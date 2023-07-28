using BashScriptRunner.HostedServices;

public class Pipeline
{
    public string? Name { get; set; }
    public List<IJob> Jobs { get; set; } = new List<IJob>();
    public PipelineState State { get; set; } = new PipelineState();
    public void Run() {}
}
