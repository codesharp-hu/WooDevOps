public class JobDescriptor
{
    public string? Name { get; set; }
    public List<JobParameter> Parameters { get; set; } = new List<JobParameter>();
}