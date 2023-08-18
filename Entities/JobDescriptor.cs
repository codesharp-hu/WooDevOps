public class JobDescriptor
{
    public string? Name { get; set; }
    public string? Command { get; set; }
    public List<JobParameter> Parameters { get; set; } = new List<JobParameter>();
}