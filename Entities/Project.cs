public class Project
{
    public string? Name { get; set; }
    public HostingEnvironment Production { get; set; } = new HostingEnvironment();
    public HostingEnvironment Staging { get; set; } = new HostingEnvironment();
    public List<PipelineDescriptor> Pipelines { get; set; } = new List<PipelineDescriptor>();
}
