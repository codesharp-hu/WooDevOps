using System.Collections.Generic;

public class PipeLineDescriptor
{
    public string? Name { get; set; }
    public List<JobDescriptor> Jobs { get; set; } = new List<JobDescriptor>();
    public List<PipeLineState>? Runs { get; set; } = new List<PipeLineState>();
    public void Run() {}
}
