using System.Threading.Channels;

namespace BashScriptRunner.Service;

public class ProjectService
{
    private readonly ApplicationDbContext dbContext;
    private readonly Channel<Pipeline> channel;
    private readonly PipelineFactory pipelineFactory = new PipelineFactory();


    public ProjectService(ApplicationDbContext dbContext, Channel<Pipeline> channel)
    {
        this.dbContext = dbContext;
        this.channel = channel;
    }
    public List<Project> ListProjects()
    {
        var projects = dbContext.Projects.ToList();
        projects.ForEach(p => dbContext.Entry(p).Collection(p => p.Pipelines).Load());
        projects.ForEach(p => p.Pipelines.ForEach(p => dbContext.Entry(p).Collection(p => p.Jobs).Load()));
        projects.ForEach(p => p.Pipelines.ForEach(p => p.Jobs.ForEach(j => dbContext.Entry(j).Collection(j => j.Parameters).Load())));
        projects.ForEach(p => p.Pipelines.ForEach(p => dbContext.Entry(p).Collection(p => p.Runs).Load()));
        projects.ForEach(p => p.Pipelines.ForEach(p => p.Runs.ForEach(r => dbContext.Entry(r).Collection(r => r.JobStates).Load())));

        return projects;
    }
    public List<PipelineState> GetProjectRuns(int projectId)
    {
        var runs = dbContext.PipelineDescriptors.Where(p => p.ProjectId == projectId).SelectMany(p => p.Runs).ToList();
        runs.ForEach(r => dbContext.Entry(r).Collection(r => r.JobStates).Load());
        return runs;
    }
    public void RunProject(int projectId)
    {
        var pipelineDescriptors = dbContext.PipelineDescriptors.Where(p => p.ProjectId == projectId).ToList();
        pipelineDescriptors.ForEach(p => dbContext.Entry(p).Collection(p => p.Jobs).Load());
        pipelineDescriptors.ForEach(p => p.Jobs.ForEach(j => dbContext.Entry(j).Collection(j => j.Parameters).Load()));
        pipelineDescriptors.ForEach(p => dbContext.Entry(p).Collection(p => p.Runs).Load());
        pipelineDescriptors.ForEach(p => p.Runs.ForEach(r => dbContext.Entry(r).Collection(r => r.JobStates).Load()));

        foreach (var pipelineDescriptor in pipelineDescriptors)
        {
            Console.WriteLine($"{pipelineDescriptor.Name} is started. Has {pipelineDescriptor.Jobs.Count} jobs.");
            var pipeline = pipelineFactory.CreatePipline(pipelineDescriptor);
            if(pipeline == null) continue;
            channel.Writer.TryWrite(pipeline);
        }
        
        dbContext.SaveChanges();
    }
}