public class PipelineDescriptor
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ProjectId { get; set; }
    public List<JobDescriptor> Jobs { get; set; } = new List<JobDescriptor>();
    public List<PipelineState> Runs { get; set; } = new List<PipelineState>();

    private static readonly PipelineFactory _pipelineFactory = new PipelineFactory();
    public void Run() {
        Console.WriteLine($"PipelineDescriptor {Name} is started.");
    }
}
