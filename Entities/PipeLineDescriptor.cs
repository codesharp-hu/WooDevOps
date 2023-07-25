using System.Collections.Generic;

public class PipeLineDescriptor
{
    public string? Name { get; set; }
    public JobDescriptor Jobs { get; set; } = new JobDescriptor();
    public List<PipeLineState>? Runs { get; set; } = new List<PipeLineState>();
    public void Run() {}
}
