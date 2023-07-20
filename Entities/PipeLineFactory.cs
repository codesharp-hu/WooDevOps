public class PipeLineFactory
{
    public PipeLine CreatePipLine(PipeLineDescriptor pipeLineDescriptor) {
        return new PipeLine {
            Name = pipeLineDescriptor.Name
        };
    }
}