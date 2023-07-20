namespace BashScriptRunner.HostedServices;

public interface IJob
{
    string Name { get; }
    PipeLineState State { get; }
    void Run();
}