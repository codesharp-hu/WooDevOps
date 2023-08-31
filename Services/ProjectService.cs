namespace BashScriptRunner.Service;

public class ProjectService
{
    private readonly ApplicationDbContext dbContext;

    public ProjectService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
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
}