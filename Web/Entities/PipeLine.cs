using WooDevOps.Web.Entities;
public class Pipeline
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Job> Jobs { get; set; } = new List<Job>();
    public PipelineState State { get; set; } = new PipelineState();

    public void Run()
    {
        Console.WriteLine($"{Name} is received, has {Jobs.Count} job.");
        foreach (var job in Jobs)
        {
            job.Run();
        }
    }
}
