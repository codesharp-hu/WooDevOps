
using WooDevOps.Web.Entities;

public class PipelineFactory
{
    public Pipeline CreatePipline(PipelineDescriptor pipelineDescriptor) {
        return new Pipeline {
            Id = pipelineDescriptor.Id,
            Name = pipelineDescriptor.Name,
            Jobs = pipelineDescriptor.Jobs.Select(jobDescriptor => new Job {
                Name = jobDescriptor.Name,
                State = new PipelineState()
            }).ToList(),
            State = new PipelineState()
        };
    }
}