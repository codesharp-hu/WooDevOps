using System.Threading.Channels;

namespace BashScriptRunner.Service;

public class PipelineService
{
    private readonly ApplicationDbContext dbContext;
    private readonly Channel<Pipeline> channel;
    private readonly PipelineFactory pipelineFactory = new PipelineFactory();


    public PipelineService(ApplicationDbContext dbContext, Channel<Pipeline> channel)
    {
        this.dbContext = dbContext;
        this.channel = channel;
    }

    public void RunPipeline(int pipelineId)
    {
        Console.WriteLine($"RunPipeline {pipelineId} is started.");
        var pipelineDescriptor = dbContext.PipelineDescriptors.FirstOrDefault(p => p.Id == pipelineId);
        if(pipelineDescriptor == null)
        {
            Console.WriteLine($"RunPipeline {pipelineId} is not found.");
            return;
        }
        dbContext.Entry(pipelineDescriptor).Collection(p => p.Jobs).Load();
        pipelineDescriptor.Jobs.ForEach(j => dbContext.Entry(j).Collection(j => j.Parameters).Load());
        dbContext.Entry(pipelineDescriptor).Collection(p => p.Runs).Load();
        pipelineDescriptor.Runs.ForEach(r => dbContext.Entry(r).Collection(r => r.JobStates).Load());

        Console.WriteLine($"{pipelineDescriptor.Name} is started. Has {pipelineDescriptor.Jobs.Count} jobs.");
        var pipeline = pipelineFactory.CreatePipline(pipelineDescriptor);
        channel.Writer.TryWrite(pipeline);
    }
}