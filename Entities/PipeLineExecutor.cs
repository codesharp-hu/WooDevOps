using System.Threading.Channels;

public class PipeLineExecutor
{
    public Channel<PipeLine>? Channel { get; set; }
    public void ExecuteAsync() {}
}
