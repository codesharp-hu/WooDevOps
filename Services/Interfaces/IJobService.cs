namespace BashScriptRunner.HostedServices;

public interface IJobService
{
    int Id { get; }
    string Name { get; }
    PipelineState State { get; }
    void Run();
}