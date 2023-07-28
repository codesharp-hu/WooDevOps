using System.Threading.Channels;

public class PipelineExecutor
{
    public Channel<Pipeline>? Channel { get; set; }
    public void ExecuteAsync() {}
}
