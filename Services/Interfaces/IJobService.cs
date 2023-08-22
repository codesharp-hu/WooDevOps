namespace BashScriptRunner.HostedServices;

public interface IJobService
{
    string Name { get; }
    PipelineState State { get; }
    void Run();
}