public class PipelineDescriptor
{
    public string? Name { get; set; }
    public List<JobDescriptor> Jobs { get; set; } = new List<JobDescriptor>();
    public List<PipelineState>? Runs { get; set; } = new List<PipelineState>();

    private static readonly PipelineFactory _pipelineFactory = new PipelineFactory();
    public void Run() {
        Pipeline pipeline = _pipelineFactory.CreatePipline(this);
        Console.WriteLine("--------------------");
        Console.WriteLine($"{pipeline.Name} is started.");
        pipeline.Run();
    }
}
