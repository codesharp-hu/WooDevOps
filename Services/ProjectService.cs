namespace BashScriptRunner.Service;

public class ProjectService
{
    public List<Project> ListProjects()
    {
        var projects = new List<Project> 
        {
            new Project 
            {
                Name = "Project 1",
                Production = new HostingEnvironment(),
                Staging = new HostingEnvironment(),
                Pipelines = new List<PipelineDescriptor>
                {
                    new PipelineDescriptor
                    {
                        Name = "PipelineDescriptor",
                        Jobs = new List<JobDescriptor>
                        {
                            new JobDescriptor
                            {
                                Name = "JobDescriptor",
                                Parameters = new List<JobParameter>
                                {
                                    new JobParameter
                                    {
                                        Name = "JobParameter",
                                        Value = "JobParameterValue"
                                    }
                                }
                            }
                        },
                        Runs = new List<PipelineState>()
                    },
                }
            }
        };
        return projects;
    }
}