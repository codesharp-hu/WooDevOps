namespace BashScriptRunner.HostedServices;

public interface IJob
{
    string Name { get; }
    PipelineState State { get; }
    void Run();
}