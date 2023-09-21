namespace WooDevOps.Web.Services;

public interface IJobService
{
    string Name { get; set; }
    PipelineState State { get; set; }
    void Run();
}