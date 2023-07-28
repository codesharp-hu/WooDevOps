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
                PipeLines = new List<PipeLineDescriptor>
                {
                    new PipeLineDescriptor
                    {
                        Name = "PipeLineDescriptor",
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
                        Runs = new List<PipeLineState>()
                    },
                }
            }
        };
        return projects;
    }
}