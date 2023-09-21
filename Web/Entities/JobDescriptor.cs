public class JobDescriptor
{
    public int Id { get; set; }
    public int PipelineDescriptorId { get; set; }
    public string? Name { get; set; }
    public List<JobParameter> Parameters { get; set; } = new List<JobParameter>();
}