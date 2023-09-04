namespace BashScriptRunner.Entities;

public interface IJobService
{
    int Id { get; set; }
    string? Name { get; set; }
    PipelineState State { get; set; }
    void Run();
}