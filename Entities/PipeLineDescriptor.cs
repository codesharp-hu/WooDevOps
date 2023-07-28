public class PipelineDescriptor
{
    public string? Name { get; set; }
    public List<JobDescriptor> Jobs { get; set; } = new List<JobDescriptor>();
    public List<PipelineState>? Runs { get; set; } = new List<PipelineState>();
    public void Run() {}
}
