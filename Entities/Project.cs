public class Project
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ProductionId { get; set; }
    public int StagingId { get; set; }
    public List<PipelineDescriptor> Pipelines { get; set; } = new List<PipelineDescriptor>();
}
