using BashScriptRunner.HostedServices;

public class PipeLine
{
    public string? Name { get; set; }
    public List<IJob> Jobs { get; set; } = new List<IJob>();
    public PipeLineState State { get; set; } = new PipeLineState();
    public void Run() {}
}
