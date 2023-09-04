namespace BashScriptRunner.Service;

public class JobService
{
    private readonly ApplicationDbContext dbContext;

    public JobService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public PipelineDescriptor? GetPipelineDescriptorById(int id)
    {
        return dbContext.PipelineDescriptors.Where(p => p.Id == id).FirstOrDefault();
    }

    public JobDescriptor? GetJobDescriptorById(int id)
    {
        return dbContext.JobDescriptors.Where(j => j.Id == id).FirstOrDefault();
    }
}