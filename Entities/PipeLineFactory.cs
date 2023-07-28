public class PipelineFactory
{
    public Pipeline CreatePipline(PipelineDescriptor pipelineDescriptor) {
        return new Pipeline {
            Name = pipelineDescriptor.Name
        };
    }
}